using PCConfigurator.Model.Components;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters;

[ValueConversion(typeof(RamType), typeof(int))]
class RamTypeMinClConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return 5;

        RamType ramType = (RamType)value;
        switch (ramType)
        {
            case RamType.DDR3:
                return 5;

            case RamType.DDR4:
                return 10;

            case RamType.DDR5:
                return 26;

            default:
                return 5;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
