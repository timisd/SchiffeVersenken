using System.Transactions;
using BattleShips.Game.Enums;
using BattleShips.Game.Helper;

namespace BattleShips.Game.Players;

public class ComputerPlayer : Player
{
    public ComputerPlayer(string name) : base(name)
    {
        
    }

    public override bool PlaceShip(ShipTypeEnum type, Position start,
        OrientationEnum orientation)
    {
        PlaceShips_1();
        // in Zukunft können mehrere oder zufallsgenerierte Layouts genutzt werden
        
        return true;
    }
    
    public override bool PlaceShot(Player enemy, Position position)
    {
        var rng = new Random();
        var possible = false;
        do
        {
            var pos = new Position(rng.Next(0, 10), rng.Next(0, 10));
            possible = base.PlaceShot(enemy, pos);
        } while (possible);

        return true;
    }

    private void PlaceShips_1()
    {
        // U-Boote
        base.PlaceShip(ShipTypeEnum.Submarine, new Position(0, 0), OrientationEnum.Horizontal);
        base.PlaceShip(ShipTypeEnum.Submarine, new Position(9, 9), OrientationEnum.Horizontal);
        // Zerstörer
        base.PlaceShip(ShipTypeEnum.Destroyer, new Position(0, 2), OrientationEnum.Vertical);
        base.PlaceShip(ShipTypeEnum.Destroyer, new Position(9, 2), OrientationEnum.Vertical);
        // Kreuzer
        base.PlaceShip(ShipTypeEnum.Cruiser, new Position(4, 3), OrientationEnum.Vertical);
        // Schlachtschiff
        base.PlaceShip(ShipTypeEnum.Battleship, new Position(6, 3), OrientationEnum.Vertical);
        // Flugzeugträger
        base.PlaceShip(ShipTypeEnum.Carrier, new Position(0, 9), OrientationEnum.Vertical);
    }
}