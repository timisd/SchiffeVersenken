using System.Windows.Controls;
using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Helper.NavigationService;

public interface INavigator
{
    UserControl? CurrentView { get; set; }
    void Navigate(UserControl? view);
}

public enum ViewsEnum
{
    Menu,
    GameSetup,
    ShipPlacement,
    Game
}