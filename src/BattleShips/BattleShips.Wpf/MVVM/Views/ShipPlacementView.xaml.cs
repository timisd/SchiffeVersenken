using System.Windows.Controls;
using System.Windows.Input;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Views;

public partial class ShipPlacementView : UserControl
{
    public ShipPlacementView()
    {
        InitializeComponent();
        AddGridButtons();
    }

    private void AddGridButtons()
    {
        for (var row = 1; row <= 10; row++)
        {
            for (var col = 1; col <= 10; col++)
            {
                var btn = new Button()
                {
                    Content = $"{row} | {col}"
                };
                
                Grid.SetRow(btn, row);
                Grid.SetColumn(btn, col);

                Ocean.Children.Add(btn);
            }
        }
    }
}