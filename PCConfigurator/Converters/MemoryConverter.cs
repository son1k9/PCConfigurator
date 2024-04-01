using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                return (gigabytes / 1000).ToString();
            else
                return gigabytes.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
