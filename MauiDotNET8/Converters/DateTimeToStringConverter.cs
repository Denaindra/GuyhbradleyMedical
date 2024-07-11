using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var dateText = date.ToString(culture.DateTimeFormat.ShortDatePattern);

            if (date.Date == DateTime.Today) dateText = "Today";
            if (date.AddDays(+1).Date == DateTime.Today) dateText = "Yesterday";
            for (int i = 2; i <= 7; i++)
            {
                if (date.AddDays(i).Date == DateTime.Today)
                {
                    dateText = date.DayOfWeek.ToString();
                    break;
                }
            }


            //return string.Format("{0} {1}", dateText, date.ToString(culture.DateTimeFormat.ShortTimePattern));
            return string.Format("{0} {1}", dateText, date.ToString("hh:mm:tt").ToUpper());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
