using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;

namespace Skylight.Messaging.Behaviors.Actions
{
    public abstract class MessageActionBase : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var associatedObject = sender as DependencyObject;
            var message = parameter as MessageBase;
            if (associatedObject == null || message == null)
            {
                return false;
            }
            return this.Execute(associatedObject, message);
        }

        protected abstract bool Execute(DependencyObject associatedObject, MessageBase message);
    }

    public abstract class MessageActionBase<T> : MessageActionBase where T : MessageBase
    {
        protected sealed override bool Execute(DependencyObject associatedObject, MessageBase message)
        {
            var typedMessage = message as T;
            return typedMessage != null && this.Execute(associatedObject, typedMessage);
        }

        protected abstract bool Execute(DependencyObject associatedObject, T message);
    }
}
