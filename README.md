# Schiffe versenken

Spiel in C# und WPF als Abschlussprojekt Praktische Informatik ISD 3.

## Spielregeln:

- Spielfeld von 10x10
- Schiffe werden platziert
- Abwechselnd können Felder gewählt werden auf die "geschossen" wird
- Wird ein Schiff getroffen darf der Spieler erneut wählen
- Spiel geht so lange nicht alle Schiffe eines Spielers zerstört sind

## Schiffstypen:

- U-Boot (Submarine):
    - Anzahl: 2
    - Göße: 1x1
- Zerstörer (Destroyer):
    - Anzahl: 2
    - Größe: 2x1
- Kreuzer (Cruiser):
    - Anzahl: 1
    - Größe: 3x1
- Schlachtschiff (Battleship):
    - Anzahl: 1
    - Größe: 4x1
- Flugzeugträger ([Aircraft]Carrier):
    - Anzahl: 1
    - Größe: 5x1

## Spielmodi:

1. Spieler gegen KI
2. Spieler gegen Spieler (am gleichen Computer)
3. [Optional] Spieler gegen Spieler (Netzwerk)
