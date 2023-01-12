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
    public int MissingSubmarinesCounter { get; private set; } = 2;
    public int MissingDestroyerCounter { get; private set; } = 2;
    public int MissingCruiserCounter { get; private set; } = 1;
    public int MissingBattleshipCounter { get; private set; } = 1;
    public int MissingCarrierCounter { get; private set; } = 1;

    private readonly List<Ship> _ships;
    private Ship? _submarine1;
    private Ship? _submarine2;
    private Ship? _destroyer1;
    private Ship? _destroyer2;
    private Ship? _cruiser;
    private Ship? _battleship;
    private Ship? _carrier;

    protected Player(string name)
    {
        Name = name;
        Board = new Grid();
        _ships = new List<Ship>();
    }

    public bool CanShipBePlaced(ShipTypeEnum type, Position start, OrientationEnum orientation)
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

        return Board.CanShipBePlaced(ship, start, orientation);
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

        var possible = Board.PlaceShip(ship, start, orientation);
        if (possible)
            MapShip(ship, type);

        return possible;
    }

    public virtual bool PlaceShot(Player enemy, Position pos)
    {
        return enemy.Board.PlaceShot(pos);
    }

    public int GetMissingShipCounter(ShipTypeEnum ship)
    {
        return ship switch
        {
            ShipTypeEnum.Submarine => MissingSubmarinesCounter,
            ShipTypeEnum.Destroyer => MissingDestroyerCounter,
            ShipTypeEnum.Cruiser => MissingCruiserCounter,
            ShipTypeEnum.Battleship => MissingBattleshipCounter,
            ShipTypeEnum.Carrier => MissingCarrierCounter,
            _ => throw new Exception("Invalid ship type")
        };
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

                MissingSubmarinesCounter--;
                break;
            case ShipTypeEnum.Destroyer:
                if (_destroyer1 == null)
                    _destroyer1 = ship;
                else
                    _destroyer2 = ship;

                MissingDestroyerCounter--;
                break;
            case ShipTypeEnum.Cruiser:
                _cruiser = ship;
                MissingCruiserCounter--;
                break;
            case ShipTypeEnum.Battleship:
                _battleship = ship;
                MissingBattleshipCounter--;
                break;
            case ShipTypeEnum.Carrier:
                _carrier = ship;
                MissingCarrierCounter--;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        _ships.Add(ship);
    }
}