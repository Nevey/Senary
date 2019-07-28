using DI;
using Gameplay.InjectionLayers;
using UnityEngine;

namespace Gameplay.Grid
{
    [Injected(Singleton = true, Layer = typeof(GameplayInjectionLayer))]
    public class GridManager
    {
        private Grid grid;
        
        public void CreateGrid()
        {
            grid = new Grid(new Vector2Int(10, 10));
        }
    }
}