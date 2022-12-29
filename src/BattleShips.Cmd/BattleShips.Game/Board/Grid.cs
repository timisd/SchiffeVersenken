using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Ships;

namespace BattleShips.Game.Board;

public class Grid
{
    public Tile[,] Ocean => (Tile[,])_ocean.Clone();

    private Tile[,] _ocean;
    
    public Grid()
    {
        // Initialisiere das Spielfeld
        _ocean = new Tile[10, 10];
        AddTiles();
    }

    /// <summary>
    /// Prüfe ob das Schiff plaziert werden kann,
    /// wenn es plaziert werden kann platziere es
    /// </summary>
    /// <param name="ship">Schiff</param>
    /// <param name="start">Startposition</param>
    /// <param name="orientation">Ausrichtung des Schiffs</param>
    /// <returns>Gibt true zurück wenn Schiff platziert ansonsten false</returns>
    /// <exception cref="ArgumentException"></exception>
    public bool PlaceShip(Ship ship, Position start, OrientationEnum orientation)
    {
        if (!ShipPlacementChecker.CanBePlaced(Ocean, ship, start, orientation))
            return false;

        switch (orientation)
        {
            case OrientationEnum.Horizontal:
                PlaceShip_Horizontal(ship, start);
                break;
            case OrientationEnum.Vertical:
                PlaceShip_Vertical(ship, start);
                break;
            default:
                throw new ArgumentException("This orientation is not present!");
        }
        
        return true;
    }

    public bool PlaceShot(Position position)
    {
        if (_ocean[position.Y, position.X].IsShot)
            return false;
        
        _ocean[position.Y, position.X].SetShot();

        return true;
    }

    private void AddTiles()
    {
        for (var row = 0; row < 10; row++)
        {
            for (var col = 0; col < 10; col++)
            {
                var pos = new Position(col, row);
                var tile = new Tile(pos);
                _ocean[row, col] = tile;
            }
        }
    }

    private void PlaceShip_Horizontal(Ship ship, Position start)
    {
        for (var offset = 0; offset < ship.Length + 1; offset++)
        {
            _ocean[start.Y, start.X + offset].PlaceShip(ship);
        }
    }

    private void PlaceShip_Vertical(Ship ship, Position start)
    {
        for (var offset = 0; offset < ship.Length + 1; offset++)
        {
            _ocean[start.Y + offset, start.X].PlaceShip(ship);
        }
    }
}