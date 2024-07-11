using MauiDotNET8.Enumerations;
using MauiDotNET8.Helpers;
using System.Globalization;

namespace MauiDotNET8.Converters
{
    public class ResponseLevelBorderColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var level = (TestResponseLevel)value;
                if (level == TestResponseLevel.Warning) return AccessResourceDictionary.GetResourcesValue("ResponseWarningBorderColor");
                if (level == TestResponseLevel.Alert) return AccessResourceDictionary.GetResourcesValue("ResponseAlertBorderColor");
            }
            return AccessResourceDictionary.GetResourcesValue("ResponseOkBorderColor");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
