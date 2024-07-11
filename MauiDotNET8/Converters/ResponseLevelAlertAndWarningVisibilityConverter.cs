using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Converters
{
    public class ResponseLevelAlertAndWarningVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((TestResponseLevel)value == TestResponseLevel.NoResponse || (TestResponseLevel)value == TestResponseLevel.Ok);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
