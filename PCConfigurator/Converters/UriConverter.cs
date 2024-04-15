using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PCConfigurator.Converters
{
    public class UriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return new BitmapImage(new Uri("pack://application:,,," + (string)value));
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
