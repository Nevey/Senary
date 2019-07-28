using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Grid
{
    public class Grid
    {
        private readonly Tile[,] tiles2D;
        private readonly List<Tile> tiles = new List<Tile>();

        public Grid(Vector2Int size)
        {
            tiles2D = new Tile[size.x, size.y];

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    Tile tile = new Tile(x, y);
                    tiles2D[x, y] = tile;
                    tiles.Add(tile);
                }
            }
        }

        public void Cleanup()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Cleanup();
            }
        }
    }
}