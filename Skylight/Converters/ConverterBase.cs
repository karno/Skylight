using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Data;

namespace Skylight.Converters
{
    // Converter templates
    public abstract class TwoWayConverter<TSource, TTarget> : ConvertBase, IValueConverter
    {
        protected abstract TTarget ToTarget(TSource input, object parameter, string language);

        protected abstract TSource ToSource(TTarget input, object parameter, string language);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ConvertSink<TSource, TTarget>(value, parameter, language, ToTarget);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ConvertSink<TTarget, TSource>(value, parameter, language, ToSource);
        }
    }

    public abstract class OneWayConverter<TSource, TTarget> : ConvertBase, IValueConverter
    {
        protected abstract TTarget ToTarget(TSource input, object parameter, string language);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ConvertSink<TSource, TTarget>(value, parameter, language, ToTarget);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }

    public abstract class ConvertBase
    {
        protected static TTarget ConvertSink<TSource, TTarget>(object value, object parameter, string language, Func<TSource, object, string, TTarget> converter)
        {
            if (IsInDesignTime)
            {
                try
                {
                    return converter((TSource)value, parameter, language);
                }
                catch
                {
                    return converter(default(TSource), parameter, language);
                }
            }
            return converter((TSource)value, parameter, language);
        }

        protected static bool IsInDesignTime
        {
            get
            {
                try
                {
                    return DesignMode.DesignModeEnabled;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
