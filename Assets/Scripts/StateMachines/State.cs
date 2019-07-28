using DI;
using Utilities;

namespace StateMachines
{
    public abstract class State
    {
        public void Initialize()
        {
            Injector.Inject(this);
        }

        public void Cleanup()
        {
            Injector.Dump(this);
        }
        
        public void Enter()
        {
            Log.Write($"{GetType().Name}");
            OnEnter();
        }

        public void Exit()
        {
            Log.Write($"{GetType().Name}");
            OnExit();
        }

        protected abstract void OnEnter();
        protected abstract void OnExit();
    }
}