using BattleShips.Game.Enums;
using BattleShips.Game.Helper;
using BattleShips.Game.Ships;

namespace BattleShips.Game.Board;

public class Tile
{
    public Position Position { get; private set; }
    public Ship? Ship { get; private set; }
    public bool IsShot { get; private set; } = false;
    public TileStatusEnum Status => GetCorrectStatus();

    public Tile(Position position)
    {
        Position = position;
    }

    /// <summary>
    /// Füge dem Feld ein Schiff hinzu
    /// </summary>
    /// <param name="ship">Schiff welches hinzugefügt wird</param>
    public void PlaceShip(Ship ship)
    {
        Ship = ship;
    }

    /// <summary>
    /// Update the IsShot Property und setzte einen Treffer bei dem Schiff wenn vorhanden
    /// </summary>
    public void SetShot()
    {
        IsShot = true;
        Ship?.AddHit(Position);
    }

    /// <summary>
    /// Gibt den korrekten Status des Feldes zurück
    /// </summary>
    /// <returns>
    /// Empty -> wenn Feld noch nicht beschossen (Ursprungszustand)
    /// Miss -> wenn Feld beschossen und kein Schiff vorhanden
    /// Hit -> wenn Feld beschossen und Schiff vorhanden
    /// Destroyed -> wenn Feld beschossen und Schiff zerstört
    /// Submarine -> wenn U-Boot vorhanden und nicht getroffen oder zerstört
    /// Destroyer -> wenn Zerstörer vorhanden und nicht getroffen oder zerstört
    /// Cruiser -> wenn Kreuzer vorhanden und nicht getroffen oder zerstört
    /// Battleship -> wenn Schlachtschiff vorhanden und nicht getroffen oder zerstört
    /// Carrier -> wenn Flugzeugträger vorhanden und nicht getroffen oder zerstört
    /// </returns>
    private TileStatusEnum GetCorrectStatus()
    {
        if (IsShot)
        {
            if (Ship != null)
            {
                return Ship.IsDestroyed ? TileStatusEnum.Destroyed : TileStatusEnum.Hit;
            }

            return TileStatusEnum.Miss;
        }

        if (Ship == null) return TileStatusEnum.Empty;
        
        var shipType = Ship.GetType().Name;
        // Erstelle ein Dictionary mit dem Klassennamen als Key und dem Enum Wert als Value
        var lookup = new Dictionary<string, TileStatusEnum>
        {
            {"Submarine", TileStatusEnum.Submarine},
            {"Destroyer", TileStatusEnum.Destroyer},
            {"Cruiser", TileStatusEnum.Cruiser},
            {"Battleship", TileStatusEnum.Battleship},
            {"Carrier", TileStatusEnum.Carrier}
        };
        
        // Schaue im Dictionary nach ob Wert vorhanden wenn nicht ist gebe leeres Feld zurück
        return lookup.TryGetValue(shipType, out var status) ? status : TileStatusEnum.Empty;
    }
}