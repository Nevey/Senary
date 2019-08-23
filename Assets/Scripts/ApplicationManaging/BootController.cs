using UnityEngine;

namespace ApplicationManaging
{
    public class BootController : MonoBehaviour
    {
        [SerializeField] private ApplicationMode applicationMode;

        private void Awake()
        {
            ApplicationManager.RegisterApplicationStates(applicationMode.ApplicationStates);
            ApplicationManager.Start(applicationMode.InitialState.State);

            // This delay is just to test
            Invoke(nameof(ToGamePlayState), 2f);
        }

        private void ToGamePlayState()
        {
            ApplicationManager.SetState(AppState.Gameplay);
        }
    }
}
