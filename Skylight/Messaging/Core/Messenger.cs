using System;
using System.Threading.Tasks;
using Skylight.Annotations;

namespace Skylight.Messaging.Core
{
    public class Messenger
    {
        public event EventHandler<MessageEventArgs> RaiseMessage;

        public async Task<T> RaiseAsync<T>([NotNull] T message) where T : MessageBase
        {
            if (message == null) throw new ArgumentNullException("message");
            var handler = this.RaiseMessage;
            var args = new MessageEventArgs(message);
            if (handler != null)
            {
                handler(this, args);
            }
            return (T)await args.CompletionSource.Task;
        }

        public async Task<TResponse> GetResponseAsync<TResponse>(
            [NotNull] ResponsiveMessageBase<TResponse> message)
        {
            var responsive = await RaiseAsync(message);
            return await responsive.CompletionSource.Task;
        }
    }
}
