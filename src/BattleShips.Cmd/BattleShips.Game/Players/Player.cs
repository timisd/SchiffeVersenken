using BattleShips.Game.Board;
using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Ships;

namespace BattleShips.Game.Players;

public abstract class Player
{
    public string Name { get; private set; }
    public Grid Board { get; private set; }
    public IEnumerable<Ship> Ships => _ships;
    public bool AllShipsPlaced => Ships.Count() == 7;
    public bool HasLost => Ships.All(ship => ship.IsDestroyed);

    private List<Ship> _ships;
    private Ship? _submarine1;
    private Ship? _submarine2;
    private Ship? _destroyer1;
    private Ship? _destroyer2;
    private Ship? _cruiser;
    private Ship? _battleship;
    private Ship? _carrier;

    public Player(string name)
    {
        Name = name;
        Board = new Grid();
        _ships = new List<Ship>();
    }

    public virtual bool PlaceShip(ShipTypeEnum type, Position start, OrientationEnum orientation)
    {
        var ship = type switch
        {
            ShipTypeEnum.Submarine => new Submarine(),
            ShipTypeEnum.Destroyer => new Destroyer(),
            ShipTypeEnum.Cruiser => new Cruiser(),
            ShipTypeEnum.Battleship => new Battleship(),
            ShipTypeEnum.Carrier => new Carrier(),
            _ => new Ship(0)
        };

        MapShip(ship, type);

        return Board.PlaceShip(ship, start, orientation);
    }

    public virtual bool PlaceShot(Player enemy, Position pos)
    {
        return enemy.Board.PlaceShot(pos);
    }

    private void MapShip(Ship ship, ShipTypeEnum type)
    {
        switch (type)
        {
            case ShipTypeEnum.Submarine:
                if (_submarine1 == null)
                    _submarine1 = ship;
                else
                    _submarine2 = ship;

                break;
            case ShipTypeEnum.Destroyer:
                if (_destroyer1 == null)
                    _destroyer1 = ship;
                else
                    _destroyer2 = ship;

                break;
            case ShipTypeEnum.Cruiser:
                _cruiser = ship;
                break;
            case ShipTypeEnum.Battleship:
                _battleship = ship;
                break;
            case ShipTypeEnum.Carrier:
                _carrier = ship;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        _ships.Add(ship);
    }
}