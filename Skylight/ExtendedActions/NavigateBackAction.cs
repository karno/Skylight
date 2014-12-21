using Microsoft.Xaml.Interactivity;
using Skylight.Internals;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Skylight.ExtendedActions
{
    public class NavigateBackAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var reference = sender as DependencyObject;
            var navigation = reference.GetNavigate() as Frame;
            if (navigation == null) return false;
            navigation.GoBack();
            return true;
        }
    }
}
