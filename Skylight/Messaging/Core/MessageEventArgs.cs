using System.Threading.Tasks;

namespace Skylight.Messaging.Core
{
    public sealed class MessageEventArgs
    {
        private readonly TaskCompletionSource<MessageBase> _completionSource;

        private readonly MessageBase _message;

        public TaskCompletionSource<MessageBase> CompletionSource
        {
            get { return this._completionSource; }
        }

        public MessageBase Message
        {
            get { return this._message; }
        }

        public MessageEventArgs(MessageBase message)
        {
            this._message = message;
            this._completionSource = new TaskCompletionSource<MessageBase>();
        }
    }
}