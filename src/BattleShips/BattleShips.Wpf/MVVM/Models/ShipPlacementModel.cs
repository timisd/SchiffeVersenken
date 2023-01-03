using BattleShips.Game.Players;

namespace BattleShips.Wpf.MVVM.Models;

public class ShipPlacementModel
{
    private Player _player;
    public ShipPlacementModel(Player player)
    {
        _player = player;
    }
}