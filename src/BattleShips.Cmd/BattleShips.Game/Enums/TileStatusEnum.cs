namespace BattleShips.Game.Enums;

public enum TileStatusEnum
{
    /// <summary>
    /// Leeres Feld
    /// </summary>
    Empty = ' ',
    /// <summary>
    /// Feld wurde beschossen
    /// </summary>
    Miss = 'o',
    /// <summary>
    /// Feld besitzt Schiff und wurde getroffen
    /// </summary>
    Hit = 'x',
    /// <summary>
    /// Feld besitz ein zerstörtes Schiff
    /// </summary>
    Destroyed = 'X',
    /// <summary>
    /// U-Boot
    /// </summary>
    Submarine = 'U',
    /// <summary>
    /// Zerstörer
    /// </summary>
    Destroyer = 'Z',
    /// <summary>
    /// Kreuzer
    /// </summary>
    Cruiser = 'K',
    /// <summary>
    /// Schlachtschiff
    /// </summary>
    Battleship = 'S',
    /// <summary>
    /// Flugzeugträger
    /// </summary>
    Carrier = 'F'
}