using PCConfigurator.Model.Components;
using System.Globalization;
using System.Windows.Data;

namespace PCConfigurator.Converters;

[ValueConversion(typeof(ICollection<Chipset>), typeof(string))]
internal class ChipsetsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string result = "";
        ICollection<Chipset> collection = (ICollection<Chipset>)value;
        List<Chipset> list = [.. collection];
        for (int i = 0; i < list.Count; i++)
        {
            result += list[i].ToString();
            if (i < list.Count - 1)
                result += ", ";
        }
        return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
