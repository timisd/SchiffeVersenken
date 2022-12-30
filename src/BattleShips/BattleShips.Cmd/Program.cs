using BattleShips.GameLoops;

Console.WriteLine("Schiffe versenken\n");
Console.WriteLine("1. Spieler gegen Computer");
Console.WriteLine("2. Spieler gegen Spieler\n");
Console.Write("Bitte tätigen Sie eine Auswahl des Modus: ");
var option = int.Parse(Console.ReadLine() ?? string.Empty);

switch (option)
{
    case 1:
        Singleplayer.Start();
        break;
    case 2:
        Multiplayer.Start();
        break;
    default:
        Environment.Exit(0);
        break;
}