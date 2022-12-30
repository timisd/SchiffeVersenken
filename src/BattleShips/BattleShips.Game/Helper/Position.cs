namespace BattleShips.Game.Helper;

public class Position
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Position(int x, int y)
    {
        // Wenn Wert größer 9 Varibale auf 9 setzen
        // Wenn Wert kleiner 0 Variable auf 0 setzen
        // sonst den Wert verwenden
        X = x > 9 ? 9 : x < 0 ? 0 : x;
        Y = y > 9 ? 9 : y < 0 ? 0 : y;
    }
}