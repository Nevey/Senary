using UnityEngine;
using UserInput;

namespace Gameplay.UserInput
{
    public class GameplayActionSet : ActionSet
    {
        private readonly AxisAction mouseX;
        private readonly AxisAction mouseY;
        private Vector2 mouseInput;

        public Vector2 MouseInput => mouseInput;
        
        public GameplayActionSet()
        {
            mouseX = CreateAxisAction("Mouse X");
            mouseY = CreateAxisAction("Mouse Y");
        }

        public override void Update()
        {
            base.Update();

            mouseInput.x = mouseX.AxisInput;
            mouseInput.y = mouseY.AxisInput;
        }
    }
}