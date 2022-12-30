using BattleShips.Game.Board;
using BattleShips.Game.Enums;

namespace BattleShips.Helper;

public static class BoardHelper
{
    public static void Print(Tile[,] ocean)
    {

        var output = Output(ocean);
        Console.WriteLine(output);
    }

    public static void HiddenPrint(Tile[,] ocean)
    {
        var toBeReplaced = new string[]
        {
            $"[{(char)TileStatusEnum.Submarine}]",
            $"[{(char)TileStatusEnum.Destroyer}]",
            $"[{(char)TileStatusEnum.Cruiser}]",
            $"[{(char)TileStatusEnum.Battleship}]",
            $"[{(char)TileStatusEnum.Carrier}]"
        };
        var replacement = "[ ]";
        var output = Output(ocean);
        output = toBeReplaced.Aggregate(output, 
            (current, replace) => 
                current.Replace(replace, replacement));
        
        Console.WriteLine(output);
    }

    private static string Output(Tile[,] ocean)
    {
        var output = "    A  B  C  D  E  F  G  H  I  J\n";
        for (var row = 0; row < 10; row++)
        {
            if (row < 9)
                output += $" {row + 1} ";
            else
                output += $"{row + 1} ";
            
            for (var col = 0; col < 10; col++)
            {
                output += $"[{(char)ocean[row, col].Status}]";
            }

            output += "\n";
        }

        return output;
    }
}