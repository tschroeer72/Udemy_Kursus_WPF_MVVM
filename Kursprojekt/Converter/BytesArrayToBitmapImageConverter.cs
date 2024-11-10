using Kursprojekt.View.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Kursprojekt.Converter;

public class BytesArrayToBitmapImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is byte[] imgBytes)
        {
            if (imgBytes != null)
            {
                return ViewsHelper.GetBitmapImageFromBytes(imgBytes);
            }
        }
        return ViewsHelper.GetEmptyBitmapImage();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}