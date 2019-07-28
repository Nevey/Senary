using DI;
using Gameplay.InjectionLayers;
using StateMachines;

namespace Gameplay.StateMachines
{
    [Injected(Layer = typeof(GameplayInjectionLayer), Singleton = true)]
    public class GameplayStateMachine : StateMachine
    {
        public GameplayStateMachine()
        {
            SetInitialState<BuildGridState>();
//            AddTransition<>();
        }
    }
}