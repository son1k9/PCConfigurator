using PCConfigurator.Model.Components;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PCConfigurator.Converters;

[ValueConversion(typeof(RamType), typeof(DoubleCollection))]
internal class RamTypeClockStepConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return new DoubleCollection();

        RamType ramType = (RamType)value;
        switch (ramType)
        {
            case RamType.DDR3:
                return new DoubleCollection() { 800, 1066, 1333, 1600, 1866, 2133 };

            case RamType.DDR4:
                return new DoubleCollection() { 1600, 1866, 2133, 2400, 2666, 2933, 3200 };

            case RamType.DDR5:
                {
                    DoubleCollection values = [];
                    for (int i = 0; i <= 13; i++)
                        values.Add(3600 + 400 * i);
                    return values;
                }

            default:
                return new DoubleCollection();
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
