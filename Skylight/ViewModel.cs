
using Skylight.Messaging.Core;

namespace Skylight
{
    public abstract class ViewModel : NotificationObject
    {
        private readonly Messenger _messenger = new Messenger();
        public Messenger Messenger
        {
            get { return this._messenger; }
        }
    }
}
