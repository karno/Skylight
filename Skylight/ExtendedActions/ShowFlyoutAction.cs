using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace Skylight.ExtendedActions
{
    public class ShowFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var element = sender as FrameworkElement;
            if (element == null) return false;
            var flyout = FlyoutBase.GetAttachedFlyout(element);
            if (flyout == null) return false;
            flyout.ShowAt(element);
            return true;
        }
    }
}
