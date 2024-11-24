using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Kursprojekt.Converter;

public class DateYearOneToEmptyDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            DateTime valDate = (DateTime)value;
            DateTime YearOneDateTime = new();
            if (valDate.ToShortDateString().Equals(YearOneDateTime.ToShortDateString()))
            {
                return string.Empty;
            }
            return valDate;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value ?? new DateTime();
    }
}