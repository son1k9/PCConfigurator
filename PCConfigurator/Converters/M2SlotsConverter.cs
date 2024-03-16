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
    [ValueConversion(typeof(ICollection<M2Slot>), typeof(string))]
    internal class M2SlotsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            ICollection<M2Slot> collection = (ICollection<M2Slot>)value;
            List<M2Slot> list = [.. collection];
            for (int i = 0; i < list.Count; i++)
            {
                result += list[i].ToString();
                if (i < list.Count - 1)
                    result += "\n";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
