using MauiDotNET8.Enumerations;
using MauiDotNET8.Helpers;
using System.Globalization;

namespace MauiDotNET8.Converters
{
    public class ResponseLevelForegoundColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var level = (TestResponseLevel)value;
            if (level == TestResponseLevel.Warning) return AccessResourceDictionary.GetResourcesValue("ResponseWarningForegroundColor");
            if (level == TestResponseLevel.Alert) return AccessResourceDictionary.GetResourcesValue("ResponseAlertForegroundColor");
            return AccessResourceDictionary.GetResourcesValue("ResponseOkForegroundColor");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
