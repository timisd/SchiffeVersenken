using BattleShips.Game.Board;
using BattleShips.Game.Enums;
using BattleShips.Game.Ships;

namespace BattleShips.Game.Helper;

public static class ShipPlacementChecker
{
    private static TileStatusEnum[] _shipList = new TileStatusEnum[]
        { TileStatusEnum.Submarine, TileStatusEnum.Destroyer, TileStatusEnum.Cruiser, TileStatusEnum.Battleship, TileStatusEnum.Carrier };
    
    public static bool CanBePlaced(Tile[,] ocean, Ship ship, Position start, OrientationEnum orientation)
    {
        return orientation switch
        {
            OrientationEnum.Horizontal => CheckIfInsideBorders(ship, start, orientation) 
                                          && !CheckIfShipToClose_Horizontal(ocean, ship, start),
            OrientationEnum.Vertical => CheckIfInsideBorders(ship, start, orientation) 
                                        && !CheckIfShipToClose_Vertical(ocean, ship, start),
            _ => false
        };
    }

    private static bool CheckIfInsideBorders(Ship ship, Position start, OrientationEnum orientation)
    {
        switch (orientation)
        {
            case OrientationEnum.Horizontal:
                if (start.X + ship.Length > 9)
                {
                    return false;
                }
                break;
            case OrientationEnum.Vertical:
                if (start.Y + ship.Length > 9)
                {
                    return false;
                }
                break;
            default:
                return false;
        }
        
        return true;
    }

    private static bool CheckIfShipToClose_Horizontal(Tile[,] ocean, Ship ship, Position start)
    {
        for (var rowOffset = 0; rowOffset < 4; rowOffset++)
        {
            for (var colOffset = -1; colOffset < ship.Length + 1; colOffset++)
            {
                if (_shipList.Contains(ocean[start.Y + rowOffset, start.X + colOffset].Status))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private static bool CheckIfShipToClose_Vertical(Tile[,] ocean, Ship ship, Position start)
    {
        for (var rowOffset = -1; rowOffset < ship.Length + 1; rowOffset++)
        {
            for (var colOffset = 0; colOffset < 4; colOffset++)
            {
                if (_shipList.Contains(ocean[start.Y + rowOffset, start.X + colOffset].Status))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}