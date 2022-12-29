using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Ships;

namespace BattleShips.Game.Board;

public class Tile
{
    public Position Position { get; private set; }
    public Ship? Ship { get; private set; }
    public bool IsShot { get; private set; } = false;
    public TileStatusEnum Status => GetCorrectStatus();

    public Tile(Position position)
    {
        Position = position;
    }

    public void PlaceShip(Ship ship)
    {
        Ship = ship;
    }

    public void SetShot()
    {
        IsShot = true;
        Ship?.AddHit(Position);
    }

    private TileStatusEnum GetCorrectStatus()
    {
        if (IsShot)
        {
            if (Ship != null)
            {
                return Ship.IsDestroyed ? TileStatusEnum.Destroyed : TileStatusEnum.Hit;
            }

            return TileStatusEnum.Miss;
        }

        if (Ship == null) return TileStatusEnum.Empty;
        
        var shipType = Ship.GetType().Name;
        var lookup = new Dictionary<string, TileStatusEnum>
        {
            {"Submarine", TileStatusEnum.Submarine},
            {"Destroyer", TileStatusEnum.Destroyer},
            {"Cruiser", TileStatusEnum.Cruiser},
            {"Battleship", TileStatusEnum.Battleship},
            {"Carrier", TileStatusEnum.Carrier}
        };
        
        return lookup.TryGetValue(shipType, out var status) ? status : TileStatusEnum.Empty;
    }
}