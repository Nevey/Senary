using UnityEngine;
using UserInput;

namespace Gameplay.UserInput
{
    public class ButtonActionSet : ActionSet
    {
        private ButtonAction mouseButtonAction;

        public ButtonAction MouseButtonAction => mouseButtonAction;

        public ButtonActionSet()
        {
            mouseButtonAction = CreateButtonAction(KeyCode.Mouse0);
        }
    }
}