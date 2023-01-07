using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Views;

public partial class ShipPlacementView : UserControl
{
    public ShipPlacementView()
    {
        InitializeComponent();
        AddGridButtons();
        AddSelectionChangeOnRadioButton();
    }
    
    private void AddGridButtons()
    {
        for (var row = 1; row <= 10; row++)
        {
            for (var col = 1; col <= 10; col++)
            {
                var btn = new Button()
                {
                    Name = $"btn{row}_{col}",
                    Content = $"{row} | {col}",
                    Background = Brushes.Transparent
                };

                btn.Click += Button_Click;
                
                Grid.SetRow(btn, row);
                Grid.SetColumn(btn, col);

                Ocean.Children.Add(btn);
            }
        }
    }
    
    private void AddSelectionChangeOnRadioButton()
    {
        foreach (RadioButton rb in ShipTypesGroup.Children)
        {
            rb.Checked += RadioButton_SelectionChange;
        }
    }

    private void RadioButton_SelectionChange(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton { IsChecked: true } rb)
        {
            (DataContext as ShipPlacementViewModel)?.ShipSelectionChanged(rb);
        }
    }
    
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        (DataContext as ShipPlacementViewModel)?.Button_Click(sender, e);
    }
}