using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace PCConfigurator.Converters
{
    public class GetIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ContentPresenter item = (ContentPresenter)value;
            ItemsControl view = ItemsControl.ItemsControlFromItemContainer(item);
            int itemIndex = view.ItemContainerGenerator.IndexFromContainer(item);

            return itemIndex;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("GetIndexMultiConverter_ConvertBack");
        }
    }
}
