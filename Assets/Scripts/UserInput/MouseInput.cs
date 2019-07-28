using System;
using DI;
using UnityEngine;
using MonoBehaviour = DI.MonoBehaviour;

namespace UserInput
{
    public class MouseInput : MonoBehaviour
    {
        private const string MOUSE_AXIS_X = "Mouse X";
        private const string MOUSE_AXIS_Y = "Mouse Y";

        [Inject] private InputController inputController;
        
        private Vector2 currentMouseInput;

        public event Action<Vector2> MouseInputEvent;

        protected override void Awake()
        {
            base.Awake();
            inputController.RegisterMouseInput(this);
            
            currentMouseInput = new Vector2(Input.GetAxis(MOUSE_AXIS_X), Input.GetAxis(MOUSE_AXIS_Y));
        }

        protected override void OnDestroy()
        {
            inputController.UnregisterMouseInput(this);
            base.OnDestroy();
        }

        private void Update()
        {
            currentMouseInput.x = Input.GetAxis(MOUSE_AXIS_X);
            currentMouseInput.y = Input.GetAxis(MOUSE_AXIS_Y);

            MouseInputEvent?.Invoke(currentMouseInput);
        }
    }
}