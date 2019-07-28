using DI;
using Gameplay.Grid;
using StateMachines;

namespace Gameplay.StateMachines
{
    public class BuildGridState : State
    {
        [Inject] private GridManager gridManager;
        
        protected override void OnEnter()
        {
            gridManager.CreateGrid();
        }

        protected override void OnExit()
        {
            
        }
    }
}