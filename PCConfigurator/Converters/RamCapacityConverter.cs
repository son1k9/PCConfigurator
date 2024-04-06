using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters;

[ValueConversion(typeof(int), typeof(int))]
internal class RamCapacityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int _value = (int)value;
        switch (_value)
        {
            case 1:
                return 0;

            case 2:
                return 1;

            case 4:
                return 2;

            case 8:
                return 3;

            case 16:
                return 4;

            case 24:
                return 5;

            case 32:
                return 6;

            case 48:
                return 7;

            case 64:
                return 8;

            case 96:
                return 9;

            case 128:
                return 10;

            case 256:
                return 11;

            default:
                return value;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int _value = (int)((double)value);
        switch (_value)
        {
            case 0:
                return 1;

            case 1:
                return 2;

            case 2:
                return 4;

            case 3:
                return 8;

            case 4:
                return 16;

            case 5:
                return 24;

            case 6:
                return 32;

            case 7:
                return 48;

            case 8:
                return 64;

            case 9:
                return 96;

            case 10:
                return 128;

            case 11:
                return 256;

            default:
                return _value;
        }
    }
}
