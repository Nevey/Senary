using DI;
using UnityEngine;
using UserInput;
using Utilities;
using MonoBehaviour = DI.MonoBehaviour;

namespace Gameplay.Cameras
{
    public class SpectatorCamera : MonoBehaviour
    {
        [Header("Camera Look Settings")]
        [SerializeField] private float minRotationX = -90f;
        [SerializeField] private float maxRotationX = 90f;

        [SerializeField] private Vector2 lookSensitivity = new Vector2(150f, 150f);

        [Header("Camera Move Settings")]
        [SerializeField] private float moveSensitivity = 0.1f;
        [SerializeField] private float moveSmoothStrength = 0.1f;

        [Inject] private InputController inputController;

        private Vector2 lookRotation;

        private Vector3 horizontalDelta;
        private Vector3 verticalDelta;
        private Vector3 upDelta;
        private Vector3 downDelta;

        private Vector3 moveDelta;
        private Vector3 targetMoveDelta;
        private Vector3 moveVelocity;

        protected override void Awake()
        {
            base.Awake();
            
            horizontalDelta = transform.position;

            inputController.LookInputEvent += OnLookInput;
            inputController.HorizontalInputEvent += OnHorizontalInput;
            inputController.VerticalInputEvent += OnVerticalInput;
            inputController.SpectatorCameraUpEvent += OnSpectatorCameraUp;
            inputController.SpectatorCameraDownEvent += OnSpectatorCameraDown;
        }

        protected override void OnDestroy()
        {
            inputController.LookInputEvent -= OnLookInput;
            inputController.HorizontalInputEvent -= OnHorizontalInput;
            inputController.VerticalInputEvent -= OnVerticalInput;
            inputController.SpectatorCameraUpEvent -= OnSpectatorCameraUp;
            inputController.SpectatorCameraDownEvent -= OnSpectatorCameraDown;
            
            base.OnDestroy();
        }

        private void Update()
        {
            targetMoveDelta = horizontalDelta + verticalDelta + upDelta + downDelta;

            moveDelta = Vector3.SmoothDamp(moveDelta, targetMoveDelta, ref moveVelocity,
                moveSmoothStrength);

            transform.position += moveDelta;
        }

        private void OnLookInput(Vector2 mouseInput)
        {
            lookRotation.x += mouseInput.y * lookSensitivity.y * Time.deltaTime;
            lookRotation.y += mouseInput.x * lookSensitivity.x * Time.deltaTime;

            lookRotation.x = ClampUtility.Angle(lookRotation.x, minRotationX, maxRotationX);

            Quaternion xQuaternion = Quaternion.AngleAxis(lookRotation.y, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(lookRotation.x, Vector3.left);

            transform.rotation = xQuaternion * yQuaternion;
        }

        private void OnHorizontalInput(float horizontalInput)
        {
            horizontalDelta = transform.right * horizontalInput * moveSensitivity;
        }

        private void OnVerticalInput(float verticalInput)
        {
            verticalDelta = transform.forward * verticalInput * moveSensitivity;
        }

        private void OnSpectatorCameraUp(float obj)
        {
            upDelta = transform.up * obj * moveSensitivity;
        }

        private void OnSpectatorCameraDown(float obj)
        {
            downDelta = (-transform.up) * obj * moveSensitivity;
        }
    }
}