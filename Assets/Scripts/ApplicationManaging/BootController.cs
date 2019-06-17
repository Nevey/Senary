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
            Invoke(nameof(ToGameState), 2f);
        }

        private void ToGameState()
        {
            ApplicationManager.SetState(ApplicationState.GameState);
        }
    }
}