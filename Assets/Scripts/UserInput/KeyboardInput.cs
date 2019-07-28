using System;
using DI;
using UnityEngine;
using UserInput;
using MonoBehaviour = DI.MonoBehaviour;

namespace UserInput
{
    public class KeyboardInput : MonoBehaviour
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const string JUMP_AXIS = "Jump";
        private const string CROUCH_AXIS = "Crouch";
        private const string SPECTATOR_CAMERA_UP = "Spectator Camera Up";
        private const string SPECTATOR_CAMERA_DOWN = "Spectator Camera Down";

        [Inject] private InputController inputController;
        
        public event Action<float> HorizontalInputEvent;
        public event Action<float> VerticalInputEvent;
        public event Action<float> JumpInputEvent;
        public event Action<float> CrouchInputEvent;
        public event Action<float> SpectatorCameraUpEvent;
        public event Action<float> SpectatorCameraDownEvent;
        public event Action ToggleConsoleEvent;

        protected override void Awake()
        {
            base.Awake();
            inputController.RegisterKeyboardInput(this);
        }

        protected override void OnDestroy()
        {
            inputController.UnregisterKeyboardInput(this);
            base.OnDestroy();
        }

        private void Update()
        {
            // TODO: Instead of continuously sending events, pick specific events which don't need to
            // be sent if input is zero
            HorizontalInputEvent?.Invoke(Input.GetAxis(HORIZONTAL_AXIS));
            VerticalInputEvent?.Invoke(Input.GetAxis(VERTICAL_AXIS));
            JumpInputEvent?.Invoke(Input.GetAxis(JUMP_AXIS));
            CrouchInputEvent?.Invoke(Input.GetAxis(CROUCH_AXIS));
            SpectatorCameraUpEvent?.Invoke(Input.GetAxis(SPECTATOR_CAMERA_UP));
            SpectatorCameraDownEvent?.Invoke(Input.GetAxis(SPECTATOR_CAMERA_DOWN));

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                ToggleConsoleEvent?.Invoke();
            }
        }
    }
}