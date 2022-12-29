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
    private Submarine _submarine1;
    private Submarine _submarine2;
    private Destroyer _destroyer1;
    private Destroyer _destroyer2;
    private Cruiser _cruiser;
    private Battleship _battleship;
    private Carrier _carrier;

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

        return Board.PlaceShip(ship, start, orientation);
    }

    public virtual bool PlaceShot(Player enemy, Position pos)
    {
        return enemy.Board.PlaceShot(pos);
    }
}