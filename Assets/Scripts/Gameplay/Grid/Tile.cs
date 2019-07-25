namespace Gameplay.Grid
{
    public class Tile
    {
        public readonly Coords coords;

        public Tile(int x, int y)
        {
            coords = new Coords(x, y);
        }
    }
}