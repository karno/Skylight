using System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace Skylight.Messaging.Behaviors.Actions
{
    public class MessageDialogAction : MessageActionBase<MessageDialogMessage>
    {
        protected override bool Execute(DependencyObject associatedObject, MessageDialogMessage message)
        {
            try
            {
                var md = new MessageDialog(message.Content)
                {
                    Title = message.Title,
                    Options = message.Options
                };
                if (message.CancelCommandIndex != null)
                {
                    md.CancelCommandIndex = (uint)message.CancelCommandIndex.Value;
                }
                if (message.DefaultCommandIndex != null)
                {
                    md.DefaultCommandIndex = (uint)message.DefaultCommandIndex.Value;
                }
                foreach (var command in message.Commands)
                {
                    md.Commands.Add(command);
                }
#pragma warning disable 4014
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    try
                    {
                        var rc = await md.ShowAsync();
                        message.CompletionSource.SetResult(new MessageDialogMessageResult(rc,
                            rc != null ? message.Commands.IndexOf(rc) : -1));
                    }
                    catch (Exception ex)
                    {
                        message.CompletionSource.SetException(ex);
                    }
                });
#pragma warning restore 4014
                return true;
            }
            catch (Exception ex)
            {
                message.CompletionSource.SetException(ex);
                return false;
            }
        }
    }
}
