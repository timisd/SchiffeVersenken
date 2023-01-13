using System.Windows;
using System.Windows.Controls;

namespace BattleShips.Wpf.MVVM.Views;

public partial class GameSetupView : UserControl
{
    public GameSetupView()
    {
        InitializeComponent();
    }

    private void GameSetupView_OnLoaded(object sender, RoutedEventArgs e)
    {
        Player1Name.Focus();
    }
}