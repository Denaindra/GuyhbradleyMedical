using MauiDotNET8.Enumerations;
using MauiDotNET8.Helpers;

namespace MauiDotNET8.Converters
{
    public class ResponseLevelBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var level = (TestResponseLevel)value;
                if (level == TestResponseLevel.Warning) return AccessResourceDictionary.GetResourcesValue("ResponseWarningBackgroundColor");
                if (level == TestResponseLevel.Alert) return AccessResourceDictionary.GetResourcesValue("ResponseAlertBackgroundColor");
            }
            return AccessResourceDictionary.GetResourcesValue("ResponseOkBackgroundColor");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
