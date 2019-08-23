using System.Linq;
using UnityEngine;
using Utilities;

namespace ApplicationManaging
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
    }
}
