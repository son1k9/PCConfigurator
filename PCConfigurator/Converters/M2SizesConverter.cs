using PCConfigurator.Model.Components.M2;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters;

[ValueConversion(typeof(M2Size), typeof(string))]
internal class M2SizesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        M2Size size = (M2Size)value;
        string result = size.ToString();
        const string symbol = "_";
        int index = result.IndexOf(symbol);
        while (index != -1)
        {
            result = result.Remove(index, 1);
            index = result.IndexOf(symbol);
        }
        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
