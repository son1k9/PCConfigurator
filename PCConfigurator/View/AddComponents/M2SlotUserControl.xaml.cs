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

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxNvme.IsChecked.Value)
            {
                if (CheckBoxSata.IsChecked.Value)
                {
                    CheckBoxNvme.IsEnabled = true;
                    CheckBoxSata.IsEnabled = true;
                }
                else
                {
                    CheckBoxNvme.IsEnabled = false;
                    CheckBoxSata.IsEnabled = true;
                }
            }
            else if (CheckBoxSata.IsChecked.Value)
            {
                CheckBoxNvme.IsEnabled = true;
                CheckBoxSata.IsEnabled = false;
            }
        }

        private void CheckBoxSize_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (var child in StackPanelSize.Children)
            {
                if (child is CheckBox checkBox)
                {
                    if (checkBox.IsChecked.Value)
                        i++;

                    checkBox.IsEnabled = true;
                }
            }

            if (i == 1)
            {
                foreach (var child in StackPanelSize.Children)
                {
                    if (child is CheckBox checkBox)
                    {
                        if (checkBox.IsChecked.Value)
                            checkBox.IsEnabled = false;
                        else
                            checkBox.IsEnabled = true;
                    }
                }
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CheckBox_Click(this, new RoutedEventArgs());
            CheckBoxSize_Click(this, new RoutedEventArgs());
        }
    }
}
