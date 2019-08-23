using System.Linq;
using UnityEngine;
using Utilities;

namespace AppManagement
{
    [CreateAssetMenu(fileName = "ApplicationMode", menuName = "Application/ApplicationMode")]
    public class ApplicationMode : ScriptableObject
    {
        [SerializeField] private ApplicationState[] applicationStates;
        [SerializeField] private ApplicationState initialState;

        public ApplicationState[] ApplicationStates => applicationStates;
        public ApplicationState InitialState => initialState;

        private void OnValidate()
        {
            if (!applicationStates.Contains(initialState))
            {
                throw Log.Exception("Initial state not included in application state list!");
            }
        }

        public ApplicationState GetApplicationState(ApplicationStateEnum stateEnum)
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
