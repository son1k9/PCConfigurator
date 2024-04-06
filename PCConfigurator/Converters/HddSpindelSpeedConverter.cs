using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters;

[ValueConversion(typeof(int), typeof(double))]
internal class HddSpindelSpeedConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int speed = (int)value;
        switch (speed)
        {
            default:
            case 3600:
                return 1;

            case 4200:
                return 2;

            case 5400:
                return 3;

            case 7200:
                return 4;

            case 10000:
                return 5;

            case 15000:
                return 6;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int speed = (int)(double)value;
        switch (speed)
        {
            default:
            case 1:
                return 3600;

            case 2:
                return 4200;

            case 3:
                return 5400;

            case 4:
                return 7200;

            case 5:
                return 10000;

            case 6:
                return 15000;
        }
    }
}
