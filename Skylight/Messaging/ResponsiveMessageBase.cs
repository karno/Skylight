using System.Threading.Tasks;

namespace Skylight.Messaging
{
    public class ResponsiveMessageBase<T> : MessageBase
    {
        private readonly TaskCompletionSource<T> _completionSource =
            new TaskCompletionSource<T>();

        public TaskCompletionSource<T> CompletionSource
        {
            get { return this._completionSource; }
        }

        public ResponsiveMessageBase(string key = null)
            : base(key)
        {

        }
    }
}
