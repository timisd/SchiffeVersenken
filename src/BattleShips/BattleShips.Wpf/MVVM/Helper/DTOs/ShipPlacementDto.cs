using BattleShips.Game.Players;
using BattleShips.Wpf.MVVM.Helper.NavigationService;

namespace BattleShips.Wpf.MVVM.Helper.DTOs;

public class ShipPlacementDto : ViewSwitchDto
{
    public Player Player1 { get; }
    public Player Player2 { get; }
    public bool SinglePlayer { get; }
    
    public ShipPlacementDto(INavigator navigator, ViewsEnum view, Player player1, Player player2, bool singlePlayer) 
        : base(navigator, view)
    {
        Player1 = player1;
        Player2 = player2;
        SinglePlayer = singlePlayer;
    }
}