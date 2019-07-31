using UnityEngine;

namespace UserInput
{
    public class ButtonAction : Action
    {
        private readonly KeyCode keyCode;
        
        private bool isUp;
        private bool isHeld;
        private bool isDown;

        /// <summary>
        /// Will be true for one frame when a button is released
        /// </summary>
        public bool IsUp => isUp;
        
        /// <summary>
        /// Will be true as long as the button is pressed
        /// </summary>
        public bool IsHeld => isHeld;
        
        /// <summary>
        /// Will be true for one frame when a button is pressed
        /// </summary>
        public bool IsDown => isDown;

        public ButtonAction(KeyCode keyCode)
        {
            this.keyCode = keyCode;
        }

        public override void Update()
        {
            isUp = Input.GetKeyUp(keyCode);
            isHeld = Input.GetKey(keyCode);
            isDown = Input.GetKeyDown(keyCode);
        }

        public override void Reset()
        {
            isUp = false;
            isHeld = false;
            isDown = false;
        }
    }
}