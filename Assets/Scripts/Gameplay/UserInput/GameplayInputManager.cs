using AppManagement;
using DI;
using UserInput;

namespace Gameplay.UserInput
{
    [Injected(Singleton = true)]
    public class GameplayInputManager : InputManager
    {
        private GameplayActionSet gameplayActionSet = new GameplayActionSet();
        private ButtonActionSet buttonActionSet = new ButtonActionSet();

        public GameplayActionSet GameplayActionSet => gameplayActionSet;
        public ButtonActionSet ButtonActionSet => buttonActionSet;

        protected override void Awake()
        {
            base.Awake();

            AddActionSet(gameplayActionSet);
            AddActionSet(buttonActionSet);
            
            gameplayActionSet.Bind();
            buttonActionSet.Bind();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}