using DI;
using StateMachines;

namespace AppManagement
{
    [Injected(Singleton = true)]
    public class ApplicationStateMachine : StateMachine
    {
        public ApplicationStateMachine()
        {
            SetInitialState<AppBootState>();

            AddTransition<AppBootState, AppGameplayState>();
        }
    }
}