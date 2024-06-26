﻿using System.Windows;

namespace PCConfigurator.View.AddComponents;

/// <summary>
/// Interaction logic for NewMotherboardWindow.xaml
/// </summary>
public partial class NewMotherboardWindow : Window
{
    public NewMotherboardWindow()
    {
        InitializeComponent();
    }

    private void AnnounceError(string message)
    {
        textBoxError.Visibility = Visibility.Visible;
        textBoxError.Text = message;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (textBoxModel.Text.Length == 0)
        {
            AnnounceError("Введите модель.");
            return;
        }
        if (comboBoxSocket.SelectedItem == null)
        {
            AnnounceError("Выберите сокет.");
            return;
        }
        if (comboBoxChipset.SelectedItem == null)
        {
            AnnounceError("Выберите чипсет.");
            return;
        }
        if (comboBoxRamType.SelectedItem == null)
        {
            AnnounceError("Выберите тип памяти.");
            return;
        }

        DialogResult = true;
    }
}
