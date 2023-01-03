using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.Helper.DTOs;

public class GameSetupDto : ViewSwitchDto
{
    public bool IsMultiplayer { get; }
    
    public GameSetupDto(INavigator navigator, ViewsEnum view, bool isMultiplayer) : base(navigator, view)
    {
        IsMultiplayer = isMultiplayer;
    }
}