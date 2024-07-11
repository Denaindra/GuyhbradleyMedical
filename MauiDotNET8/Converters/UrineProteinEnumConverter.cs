using MauiDotNET8.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Converters
{
    public class UrineProteinEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((UrineProteinLevel)value)
            {
                case UrineProteinLevel.Trace:
                    return "Trace";
                case UrineProteinLevel.OneStar:
                    return "1*";
                case UrineProteinLevel.TwoStars:
                    return "2**";
                case UrineProteinLevel.ThreeStars:
                    return "3***";
                default:
                    return "Nil";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
