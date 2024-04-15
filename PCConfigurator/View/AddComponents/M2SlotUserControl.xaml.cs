using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
