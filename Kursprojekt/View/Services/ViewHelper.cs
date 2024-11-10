using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Kursprojekt.View.Services;

public class ViewsHelper
{
    public static BitmapImage GetBitmapImageFromBytes(byte[]? bBytePhoto)
    {
        var imgHaus = new BitmapImage();
        try
        {
            if (bBytePhoto != null && bBytePhoto?.Length is not 0)
            {
                using var stream = new MemoryStream(bBytePhoto!);
                imgHaus.BeginInit();
                imgHaus.CacheOption = BitmapCacheOption.OnLoad;
                imgHaus.StreamSource = stream;
                imgHaus.EndInit();
            }
            return imgHaus;
        }
        catch
        {
            return imgHaus;
        }
    }
    public static BitmapImage GetEmptyBitmapImage()
    {
        return new BitmapImage();
    }
    public static BitmapImage GetBitmapImageFromFileName(string fFilename)
    {
        var imgHaus = new BitmapImage();
        if (fFilename == null) return imgHaus;

        return new BitmapImage(new Uri(fFilename));
    }

}