using UnityEngine;

namespace ApplicationManaging
{
    [CreateAssetMenu(fileName = "ApplicationMode", menuName = "Application/ApplicationMode")]
    public class ApplicationMode : ScriptableObject
    {
        [SerializeField] private ApplicationState initialState;

        public ApplicationState InitialState => initialState;
    }
}