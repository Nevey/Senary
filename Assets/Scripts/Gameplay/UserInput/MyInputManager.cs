using DI;
using UserInput;
using Utilities;

namespace Gameplay.UserInput
{
    [Injected(Singleton = true)]
    public class MyInputManager : InputManager
    {
        // TODO: Bind/Unbind action sets based on current application state
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
            
            Log.Write(buttonActionSet.MouseButtonAction.IsDown);
        }
    }
}