using System.Collections;
using System.Linq;
using Windows.UI.Xaml;

namespace Skylight.Converters
{
    public class EnumerableExistsToVisibilityConverter : OneWayConverter<IEnumerable, Visibility>
    {
        protected override Visibility ToTarget(IEnumerable input, object parameter, string language)
        {
            var exist = false;
            var list = input as IList;
            if (list != null)
            {
                exist = list.Count > 0;
            }
            else if (input != null)
            {
                exist = input.Cast<object>().Any();
            }
            return exist ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public class InverseEnumerableExistsToVisibilityConverter : OneWayConverter<IEnumerable, Visibility>
    {
        protected override Visibility ToTarget(IEnumerable input, object parameter, string language)
        {
            var exist = false;
            var list = input as IList;
            if (list != null)
            {
                exist = list.Count > 0;
            }
            else if (input != null)
            {
                exist = input.Cast<object>().Any();
            }
            return exist ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
