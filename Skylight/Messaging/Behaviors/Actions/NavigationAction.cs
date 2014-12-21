using Skylight.Internals;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Skylight.Messaging.Behaviors.Actions
{
    public class NavigationAction : MessageActionBase<NavigationMessage>
    {
        protected override bool Execute(DependencyObject associatedObject, NavigationMessage message)
        {
            var navigate = associatedObject.GetNavigate();
            if (navigate == null)
            {
                return false;
            }
            var frame = navigate as Frame;
            return frame != null
                ? frame.Navigate(message.NavigationPageType, message.Arguments)
                : navigate.Navigate(message.NavigationPageType);
        }
    }
}
