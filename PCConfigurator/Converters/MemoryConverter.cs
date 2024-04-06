using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    class MemoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int gigabytes = (int)value;

            if (gigabytes > 1024)
                return (gigabytes / 1000).ToString() + " ТБ";
            else
                return gigabytes.ToString() + " ГБ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
