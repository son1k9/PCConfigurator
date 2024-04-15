using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PCConfigurator.View;

/// <summary>
/// Interaction logic for ComponentUserControl.xaml
/// </summary>
public partial class ComponentUserControl : UserControl
{
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ComponentUserControl));
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(ComponentUserControl));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(string), typeof(ComponentUserControl));
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(ComponentUserControl));
    public static readonly DependencyProperty ShowHeaderProperty = DependencyProperty.Register(nameof(ShowHeader), typeof(bool), typeof(ComponentUserControl));

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

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public bool ShowHeader
    {
        get => (bool)GetValue(ShowHeaderProperty);
        set => SetValue(ShowHeaderProperty, value);
    }

    public ComponentUserControl()
    {
        InitializeComponent();
    }
}
