using Battleships.MVVM.ViewModels;

namespace Battleships.Services.Navigation;

internal interface INavigator
{
    BaseViewModel? CurrentViewModel { get; set; }
    void Navigate(BaseViewModel? viewModel);
}

public enum ViewsEnum
{
    Menu,
    Options,
    ShipPlacement
}