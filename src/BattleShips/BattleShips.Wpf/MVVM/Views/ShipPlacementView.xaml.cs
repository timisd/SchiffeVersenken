using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Views;

public partial class ShipPlacementView : UserControl
{
    public ShipPlacementView()
    {
        InitializeComponent();
        AddSelectionChangeOnRadioButton();
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
}