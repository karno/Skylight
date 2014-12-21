using System;

namespace Skylight.Messaging
{
    public sealed class NavigationMessage : MessageBase
    {
        public static NavigationMessage Create<T>(object argument = null)
        {
            return new NavigationMessage(typeof(T), argument);
        }

        public NavigationMessage(string messageKey, Type navigationPageType, object arguments)
            : base(messageKey)
        {
            this.NavigationPageType = navigationPageType;
            this.Arguments = arguments;
        }

        public NavigationMessage(Type navigationPageType = null, object arguments = null)
            : this(null, navigationPageType, arguments)
        {
        }

        public Type NavigationPageType { get; set; }

        public object Arguments { get; set; }
    }
}
