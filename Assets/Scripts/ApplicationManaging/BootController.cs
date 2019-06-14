using UnityEngine;

namespace ApplicationManaging
{
    public class BootController : MonoBehaviour
    {
        [SerializeField] private ApplicationMode applicationMode;
        
        private void Awake()
        {
            ApplicationManager.SetState(applicationMode.InitialState);
            
            Invoke(nameof(ToGameState), 2f);
        }

        private void ToGameState()
        {
            ApplicationManager.SetState(ApplicationState.GameState);
        }
    }
}