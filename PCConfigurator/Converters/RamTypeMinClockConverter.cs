using PCConfigurator.Model.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PCConfigurator.Converters;

[ValueConversion(typeof(RamType), typeof(int))]
internal class RamTypeMinClockConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return 800;

        RamType ramType = (RamType)value;
        switch (ramType)
        {
            case RamType.DDR3:
                return 800;

            case RamType.DDR4:
                return 1600;

            case RamType.DDR5:
                return 3600;

            default:
                return 800;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
