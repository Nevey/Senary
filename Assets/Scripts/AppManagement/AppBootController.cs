using UnityEngine;

namespace AppManagement
{    
    public class AppBootController : MonoBehaviour
    {
        [SerializeField] private AppModeConfig appModeConfig;

        private void Awake()
        {
            AppManager.Setup(appModeConfig);
            AppManager.Start();

            ToGameplayState();
        }

        private void ToGameplayState()
        {
            AppManager.SetState(AppStateEnum.Gameplay);
        }
    }
}