using DI;
using Gameplay.StateMachines;

namespace Gameplay
{
    public class GameplayStarter : MonoBehaviour
    {
        [Inject] private GameplayStateMachine gameplayStateMachine;
        
        protected override void Start()
        {
            base.Start();
            gameplayStateMachine.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            gameplayStateMachine.Stop();
        }
    }
}