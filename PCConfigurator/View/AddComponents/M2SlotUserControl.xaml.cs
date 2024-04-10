using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCConfigurator.View.AddComponents
{
    /// <summary>
    /// Interaction logic for M2SlotUserControl.xaml
    /// </summary>
    public partial class M2SlotUserControl : UserControl
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(M2SlotUserControl));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(M2SlotUserControl));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }


        public M2SlotUserControl()
        {
            InitializeComponent();
        }
    }
}
