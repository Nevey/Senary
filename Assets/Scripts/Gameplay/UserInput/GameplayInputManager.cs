using DI;
using UserInput;

namespace Gameplay.UserInput
{
    [Injected(Singleton = true)]
    public class GameplayInputManager : InputManager
    {
        private GameplayActionSet gameplayActionSet = new GameplayActionSet();
        private MouseButtonActionSet mouseButtonActionSet = new MouseButtonActionSet();

        public GameplayActionSet GameplayActionSet => gameplayActionSet;
        public MouseButtonActionSet ButtonActionSet => mouseButtonActionSet;

        protected override void Awake()
        {
            base.Awake();

            AddActionSet(gameplayActionSet);
            AddActionSet(mouseButtonActionSet);
        }
    }
}