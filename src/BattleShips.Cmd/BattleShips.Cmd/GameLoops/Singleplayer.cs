using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Players;
using BattleShips.Helper;

namespace BattleShips.GameLoops;

public static class Singleplayer
{
    public static void Start()
    {
        Console.Clear();
        Console.Write("Bitte gib deinen Namen ein: ");
        var name = Console.ReadLine();
        // Spieler initialisieren
        var player1 = new HumanPlayer(name);
        var player2 = new ComputerPlayer("Computer 1");

        // Computer setzt Schiffe
        player2.PlaceShip();
        while (!player1.AllShipsPlaced)
        {
            switch (player1.Ships.Count())
            {
                case 0:
                    PlaceSubmarine(player1);
                    break;
                case 1:
                    PlaceSubmarine(player1);
                    break;
                case 2:
                    PlaceDestroyer(player1);
                    break;
                case 3:
                    PlaceDestroyer(player1);
                    break;
                case 4:
                    PlaceCruiser(player1);
                    break;
                case 5:
                    PlaceBattleship(player1);
                    break;
                case 6:
                    PlaceCarrier(player1);
                    break;
                default:
                    throw new Exception("Scheint als gäbe es zu viele Schiffe");
            }
        }
        
        BoardHelper.Print(player1.Board.Ocean);
    }

    private static void PlaceSubmarine(Player player)
    {
        while (true)
        {
            var pos = GetShipPosition(player, "U-Boot");
            var orientation = GetShipOrientation();
            if (player.PlaceShip(ShipTypeEnum.Submarine, pos, orientation))
            {
                return;
            }
        }
    }

    private static void PlaceDestroyer(Player player)
    {
        while (true)
        {
            var pos = GetShipPosition(player, "Zerstörer");
            var orientation = GetShipOrientation();
            if (player.PlaceShip(ShipTypeEnum.Destroyer, pos, orientation))
            {
                return;
            }
        }
    }
    
    private static void PlaceCruiser(Player player)
    {
        while (true)
        {
            var pos = GetShipPosition(player, "Kreuzer");
            var orientation = GetShipOrientation();
            if (player.PlaceShip(ShipTypeEnum.Cruiser, pos, orientation))
            {
                return;
            }
        }
    }
    
    private static void PlaceBattleship(Player player)
    {
        while (true)
        {
            var pos = GetShipPosition(player, "Schlachtschiff");
            var orientation = GetShipOrientation();
            if (player.PlaceShip(ShipTypeEnum.Battleship, pos, orientation))
            {
                return;
            }
        }
    }
    
    private static void PlaceCarrier(Player player)
    {
        while (true)
        {
            var pos = GetShipPosition(player, "Flugzeugträger");
            var orientation = GetShipOrientation();
            if (player.PlaceShip(ShipTypeEnum.Carrier, pos, orientation))
            {
                return;
            }
        }
    }

    private static Position GetShipPosition(Player player, string shipType)
    {
        int row;
        int col;
        do
        {
            Console.Clear();
            BoardHelper.Print(player.Board.Ocean);
            Console.WriteLine($"Wo möchtest du dein {shipType} paltzieren?");
            var input = Console.ReadLine();
            col = input[0] - 'A';
            input = input.Substring(1);
            row = int.Parse(input.ToString()) - 1;
        } while (!(row is > -1 and < 11 && col is > -1 and < 11));
        
        return new Position(col, row);
    }

    private static OrientationEnum GetShipOrientation()
    {
        int selection;
        do
        {
            Console.WriteLine("Wie soll das Schiff ausgerichtet sein?\n" +
                              "1. Horizontal\n" +
                              "2. Vertikal");
            selection = int.Parse(Console.ReadLine() ?? string.Empty);
        } while (selection != 1 && selection != 2);

        return selection == 1 ? OrientationEnum.Horizontal : OrientationEnum.Vertical;
    }
}