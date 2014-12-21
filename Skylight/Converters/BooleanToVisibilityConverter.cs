using Windows.UI.Xaml;

namespace Skylight.Converters
{
    public class BooleanToVisibilityConverter : TwoWayConverter<bool, Visibility>
    {
        protected override Visibility ToTarget(bool input, object parameter, string language)
        {
            return input ? Visibility.Visible : Visibility.Collapsed;
        }

        protected override bool ToSource(Visibility input, object parameter, string language)
        {
            return input == Visibility.Visible;
        }
    }

    public class InverseBooleanToVisibilityConverter : TwoWayConverter<bool, Visibility>
    {
        protected override Visibility ToTarget(bool input, object parameter, string language)
        {
            return input ? Visibility.Collapsed : Visibility.Visible;
        }

        protected override bool ToSource(Visibility input, object parameter, string language)
        {
            return input == Visibility.Collapsed;
        }
    }
}
