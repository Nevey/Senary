using DI;
using Gameplay.Grid;
using StateMachines;

namespace Gameplay.StateMachines
{
    public class ClearGridState : State
    {
        [Inject] private GridManager gridManager;
        
        protected override void OnEnter()
        {
            gridManager.Cleanup();
        }

        protected override void OnExit()
        {
            
        }
    }
}