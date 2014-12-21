using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Skylight.Messaging.Behaviors.Actions
{
    public class WebViewNavigationAction : MessageActionBase<WebViewNavigationMessage>
    {
        protected override bool Execute(DependencyObject associatedObject, WebViewNavigationMessage message)
        {
            var webView = associatedObject as WebView;
            if (webView == null) return false;
            try
            {
                webView.Navigate(message.NavigationUri);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
