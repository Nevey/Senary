using DI;
using Factories;
using Gameplay.Grid;
using Gameplay.InjectionLayers;

namespace Gameplay.Factories
{
    [Injected(Layer = typeof(GameplayInjectionLayer), Singleton = true)]
    public class TileViewFactory : Factory<TileView>
    {
        
    }
}