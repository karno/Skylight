using System;
using Microsoft.Xaml.Interactivity;
using Skylight.Messaging.Core;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace Skylight.Messaging.Behaviors
{
    [ContentProperty(Name = "Actions")]
    public class MessageTriggerBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register("Actions",
            typeof(ActionCollection), typeof(MessageTriggerBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty MessageKeyProperty = DependencyProperty.Register(
            "MessageKey", typeof(string), typeof(MessageTriggerBehavior), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty MessengerProperty = DependencyProperty.Register("Messenger",
            typeof(Messenger), typeof(MessageTriggerBehavior), new PropertyMetadata(null, MessengerChanged));

        private static void MessengerChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var messenger = e.NewValue as Messenger;
            if (e.NewValue != null && messenger == null)
            {
                throw new InvalidOperationException(
                    "Bound object is not a Messenger in MessageTriggerProperty.Messenger.");
            }
            ((MessageTriggerBehavior)sender).MessengerChanged(messenger);
        }

        // this variable is NOT synchronized with Messenger property.
        private Messenger _boundMessenger;

        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection)GetValue(ActionsProperty);
                if (actions == null)
                {
                    actions = new ActionCollection();
                    this.SetValue(ActionsProperty, actions);
                }
                return actions;
            }
        }

        public string MessageKey
        {
            get { return (string)GetValue(MessageKeyProperty); }
            set { SetValue(MessageKeyProperty, value); }
        }

        public Messenger Messenger
        {
            get { return (Messenger)this.GetValue(MessengerProperty); }
            set { this.SetValue(MessengerProperty, value); }
        }

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            if (this.AssociatedObject == associatedObject || DesignMode.DesignModeEnabled)
            {
                return;
            }
            if (this.AssociatedObject != null)
            {
                throw new InvalidOperationException(
                    "Could not attach the behavior for multiple times.");
            }
            this.AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            this.AssociatedObject = null;
        }

        private void MessengerChanged(Messenger sender)
        {
            if (sender == this._boundMessenger) return;
            if (this._boundMessenger != null)
            {
                this._boundMessenger.RaiseMessage -= MessengerOnRaiseMessage;
            }
            this._boundMessenger = sender;
            if (this._boundMessenger != null)
            {
                this._boundMessenger.RaiseMessage += this.MessengerOnRaiseMessage;
            }
        }

        private async void MessengerOnRaiseMessage(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (this.MessageKey != null && this.MessageKey != e.Message.Key)
            {
                return;
            }
            try
            {
                if (Dispatcher.HasThreadAccess)
                {
                    InvokeActions(msg);
                }
                else
                {
                    // queue on dispatcher
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => InvokeActions(msg));
                }
                e.CompletionSource.TrySetResult(msg);
            }
            catch (Exception ex)
            {
                e.CompletionSource.TrySetException(ex);
            }
        }

        private void InvokeActions(MessageBase message)
        {
            var associated = this.AssociatedObject;
            if (associated != null)
            {
                Interaction.ExecuteActions(associated, Actions, message);
            }
        }
    }
}
