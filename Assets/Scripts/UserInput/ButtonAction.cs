using UnityEngine;

namespace UserInput
{
    public class ButtonAction : Action
    {
        private readonly KeyCode keyCode;
        
        private bool isReleased;
        private bool isHeld;
        private bool isPressed;

        /// <summary>
        /// Will be true for one frame when a button is pressed
        /// </summary>
        public bool IsPressed => isPressed;

        /// <summary>
        /// Will be true as long as the button is pressed
        /// </summary>
        public bool IsHeld => isHeld;

        /// <summary>
        /// Will be true for one frame when a button is released
        /// </summary>
        public bool IsReleased => isReleased;

        public ButtonAction(KeyCode keyCode)
        {
            this.keyCode = keyCode;
        }

        public override void Update()
        {
            isPressed = Input.GetKeyDown(keyCode);
            isHeld = Input.GetKey(keyCode);
            isReleased = Input.GetKeyUp(keyCode);
        }

        public override void Reset()
        {
            isPressed = false;
            isHeld = false;
            isReleased = false;
        }
    }
}