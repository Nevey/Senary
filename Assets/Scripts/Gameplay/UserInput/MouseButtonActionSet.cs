using UnityEngine;
using UserInput;

namespace Gameplay.UserInput
{
    public class MouseButtonActionSet : ActionSet
    {
        private ButtonAction mouseButtonAction;

        public ButtonAction MouseButtonAction => mouseButtonAction;

        public MouseButtonActionSet()
        {
            mouseButtonAction = CreateButtonAction(KeyCode.Mouse0);
        }
    }
}