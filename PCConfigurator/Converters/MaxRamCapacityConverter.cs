using PCConfigurator.Model.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    [ValueConversion(typeof(int), typeof(int))]
    class MaxRamCapacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _value = (int)value;
            return (int)Math.Log2(_value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _value = (int)(double)value;
            return (int)Math.Pow(2, _value);
        }
    }
}
