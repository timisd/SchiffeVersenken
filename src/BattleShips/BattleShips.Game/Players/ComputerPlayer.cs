using System.Transactions;
using BattleShips.Game.Enums;
using BattleShips.Game.Helper;

namespace BattleShips.Game.Players;

public class ComputerPlayer : Player
{
    public ComputerPlayer(string name) : base(name)
    {
        
    }

    public bool PlaceShip()
    {
        PlaceShips_1();
        // in Zukunft können mehrere oder zufallsgenerierte Layouts genutzt werden
        
        return true;
    }
    
    public bool PlaceShot(Player enemy)
    {
        var rng = new Random();
        var possible = false;
        do
        {
            var pos = new Position(rng.Next(0, 10), rng.Next(0, 10));
            possible = base.PlaceShot(enemy, pos);
        } while (!possible);

        return true;
    }

    private void PlaceShips_1()
    {
        // U-Boote
        base.PlaceShip(ShipTypeEnum.Submarine, new Position(0, 0), OrientationEnum.Horizontal); // A1
        base.PlaceShip(ShipTypeEnum.Submarine, new Position(9, 9), OrientationEnum.Horizontal); // J10
        // Zerstörer
        base.PlaceShip(ShipTypeEnum.Destroyer, new Position(0, 2), OrientationEnum.Vertical); // A3, A4
        base.PlaceShip(ShipTypeEnum.Destroyer, new Position(9, 2), OrientationEnum.Vertical); // J3, J4
        // Kreuzer
         base.PlaceShip(ShipTypeEnum.Cruiser, new Position(4, 3), OrientationEnum.Vertical); // E4, E5, E6
        // Schlachtschiff
         base.PlaceShip(ShipTypeEnum.Battleship, new Position(6, 3), OrientationEnum.Vertical); // G4, G5, G6, G7
        // Flugzeugträger
         base.PlaceShip(ShipTypeEnum.Carrier, new Position(0, 9), OrientationEnum.Horizontal); // A10, B10, C10, D10, E10
    }
}