using System.Linq;
using UnityEngine;
using Utilities;

namespace AppManagement
{
    [CreateAssetMenu(fileName = "AppModeConfig", menuName = "AppManagement/AppModeConfig")]
    public class AppModeConfig : ScriptableObject
    {
        [SerializeField] private AppStateConfig[] applicationStates;
        [SerializeField] private AppStateConfig initialState;

        public AppStateConfig[] ApplicationStates => applicationStates;
        public AppStateConfig InitialState => initialState;

        private void OnValidate()
        {
            if (!applicationStates.Contains(initialState))
            {
                throw Log.Exception("Initial state not included in application state list!");
            }
        }

        public AppStateConfig GetApplicationState(AppStateEnum stateEnum)
        {
            for (int i = 0; i < applicationStates.Length; i++)
            {
                if (applicationStates[i].StateEnum == stateEnum)
                {
                    return applicationStates[i];
                }
            }

            throw Log.Exception($"No ApplicationState found with Enum value {stateEnum}");
        }
    }
}
