using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.Helper.DTOs;

public class ViewSwitchDto
{
    public INavigator Navigator { get; }
    public ViewsEnum View { get; }

    public ViewSwitchDto(INavigator navigator, ViewsEnum view)
    {
        Navigator = navigator;
        View = view;
    }
}