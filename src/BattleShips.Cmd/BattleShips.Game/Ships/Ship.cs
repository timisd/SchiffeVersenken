using BattleShips.Game.Helper;

namespace BattleShips.Game.Ships;

public class Ship
{
    /// <summary>
    /// Länge des Schiffes
    /// </summary>
    public int Length { get; private set; }
    /// <summary>
    /// Zur Abfrage der Bereits getroffenen Felder des Schiffes
    /// </summary>
    public IEnumerable<Position> Hits => _hits;
    /// <summary>
    /// Ist das Schiff zerstört?
    /// </summary>
    public bool IsDestroyed => Hits.Count() >= Length;

    /// <summary>
    /// Wo wurde das Schiff bis jetzt getroffen
    /// Noch keinen weiteren Nutzen, könnte auch int sein
    /// </summary>
    private List<Position> _hits;

    public Ship(int length)
    {
        Length = length;
        _hits = new List<Position>();
    }
    
    /// <summary>
    /// Treffer hinzufügen
    /// </summary>
    /// <param name="position">Position wo getroffen wurde</param>
    public void AddHit(Position position)
    {
        _hits.Add(position);
    }
}