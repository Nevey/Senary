using DI;
using Gameplay.Factories;

namespace Gameplay.Grid
{
    public class Tile
    {
        [Inject] private TileViewFactory tileViewFactory;

        private TileView tileView;
        
        public readonly Coords coords;

        public Tile(int x, int y)
        {
            Injector.Inject(this);

            coords = new Coords(x, y);
            
            tileView = (TileView)tileViewFactory.Instantiate();
            tileView.SetPosition(coords); 
        }
    }
}