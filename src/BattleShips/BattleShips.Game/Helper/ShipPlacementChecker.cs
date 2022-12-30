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

    /// <summary>
    /// Prüfen ob das Schiff durch seine Länge aus dem Spielfeld ragt
    /// </summary>
    /// <param name="ship">Schiff</param>
    /// <param name="start">Startposition</param>
    /// <param name="orientation">Ausrichtung</param>
    /// <returns>Wahr wenn das Schiff im Spielfeld liegt sonst falsch</returns>
    private static bool CheckIfInsideBorders(Ship ship, Position start, OrientationEnum orientation)
    {
        switch (orientation)
        {
            case OrientationEnum.Horizontal:
                if (start.X + ship.Length - 1 > 9)
                {
                    return false;
                }
                break;
            case OrientationEnum.Vertical:
                if (start.Y + ship.Length - 1 > 9)
                {
                    return false;
                }
                break;
            default:
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// Horizontaler Check
    /// Prüfen ob in einem Umkreis von einem Feld,
    /// um die gewüschte Schiffposition, ein weiteres Schiff liegt
    /// </summary>
    /// <param name="ocean">Spielfeld</param>
    /// <param name="ship">Schiff</param>
    /// <param name="start">Startposition</param>
    /// <returns>Flasch wenn kein Schiff zu nah sonst Wahr</returns>
    private static bool CheckIfShipToClose_Horizontal(Tile[,] ocean, Ship ship, Position start)
    {
        for (var rowOffset = -1; rowOffset < 2; rowOffset++)
        {
            for (var colOffset = -1; colOffset < ship.Length + 1; colOffset++)
            {
                if (start.X + colOffset < 0 || start.X + colOffset > 9 || start.Y + rowOffset < 0 || start.Y + rowOffset > 9)
                {
                    continue;
                }
                
                if (_shipList.Contains(ocean[start.Y + rowOffset, start.X + colOffset].Status))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// Vertikaler Check
    /// Prüfen ob in einem Umkreis von einem Feld,
    /// um die gewüschte Schiffposition, ein weiteres Schiff liegt
    /// </summary>
    /// <param name="ocean">Spielfeld</param>
    /// <param name="ship">Schiff</param>
    /// <param name="start">Startposition</param>
    /// <returns>Flasch wenn kein Schiff zu nah sonst Wahr</returns>
    private static bool CheckIfShipToClose_Vertical(Tile[,] ocean, Ship ship, Position start)
    {
        for (var rowOffset = -1; rowOffset < ship.Length + 1; rowOffset++)
        {
            for (var colOffset = -1; colOffset < 2; colOffset++)
            {
                if (start.X + colOffset < 0 || start.X + colOffset > 9 || start.Y + rowOffset < 0 || start.Y + rowOffset > 9)
                {
                    continue;
                }
                
                if (_shipList.Contains(ocean[start.Y + rowOffset, start.X + colOffset].Status))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}