using BattleShips.Wpf.MVVM.ViewModels;

namespace BattleShips.Wpf.MVVM.Helper.NavigationService;

public interface INavigator
{
    BaseViewModel? CurrentViewModel { get; set; }
    void Navigate(BaseViewModel? viewModel);
}

public enum ViewsEnum
{
    Menu,
    GameSetup,
    ShipPlacement
}