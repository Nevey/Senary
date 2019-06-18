using DependencyInjection.Attributes;
using Gameplay.InjectionLayers;

namespace Gameplay
{
    [Injected(Layer = typeof(GameplayInjectionLayer))]
    public class Test
    {
        public string testString;
    }
}