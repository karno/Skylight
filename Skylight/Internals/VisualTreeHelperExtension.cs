using Skylight.Annotations;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Skylight.Internals
{
    public static class VisualTreeHelperExtension
    {
        public static INavigate GetNavigate()
        {
            return GetNavigate(null);
        }

        public static INavigate GetNavigate([CanBeNull] this DependencyObject reference)
        {
            var navigate = Window.Current.Content as INavigate;
            return navigate ?? reference.FindParent<INavigate>();
        }

        public static Page GetPage()
        {
            return GetPage(null);
        }


        public static Page GetPage([CanBeNull] this DependencyObject reference)
        {
            var page = reference as Page;
            if (page != null)
            {
                return page;
            }
            var frame = GetNavigate(reference) as Frame;
            if (frame != null)
            {
                page = frame.Content as Page;
                if (page != null)
                {
                    return page;
                }
            }
            return reference.FindParent<Page>();
        }

        public static T FindParent<T>(this DependencyObject reference) where T : class
        {
            while (reference != null)
            {
                var result = reference as T;
                if (result != null)
                {
                    return result;
                }
                reference = VisualTreeHelper.GetParent(reference);
            }
            return null;
        }
    }
}
