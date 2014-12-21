using System;

namespace Skylight.Messaging
{
    public class WebViewNavigationMessage : MessageBase
    {
        private readonly Uri _navigationUri;

        public WebViewNavigationMessage(string navigationUriString)
            : this(new Uri(navigationUriString))
        {

        }

        public WebViewNavigationMessage(Uri navigationUri)
        {
            _navigationUri = navigationUri;
        }

        public WebViewNavigationMessage(string key, string navigationUriString)
            : this(key, new Uri(navigationUriString))
        {

        }

        public WebViewNavigationMessage(string key, Uri navigationUri)
            : base(key)
        {
            this._navigationUri = navigationUri;
        }

        public Uri NavigationUri
        {
            get { return this._navigationUri; }
        }
    }
}
