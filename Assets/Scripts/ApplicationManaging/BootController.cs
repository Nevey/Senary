using UnityEngine;

namespace ApplicationManaging
{
    public class BootController : MonoBehaviour
    {
        [SerializeField] private ApplicationMode applicationMode;
        
        private void Awake()
        {
            ApplicationManager.SetState(applicationMode.InitialState);
            
            // This delay is just to test 
            Invoke(nameof(ToGamePlayState), 2f);
        }

        private void ToGamePlayState()
        {
            ApplicationManager.SetState(ApplicationState.GamePlayState);
        }
    }
}