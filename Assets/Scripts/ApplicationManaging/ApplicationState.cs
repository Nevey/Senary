using System;
using UnityEngine;
using Utilities;

namespace ApplicationManaging
{
    public enum AppState
    {
        Boot,
        Gameplay,
    }

    [CreateAssetMenu(fileName = "ApplicationState", menuName = "ApplicationManaging/ApplicationState")]
    public class ApplicationState : ScriptableObject
    {
        [SerializeField] private SceneReference scene;
        [SerializeField] private AppState state;
        [SerializeField, HideInInspector] private bool useCustomInjectionLayers;
        [SerializeField, HideInInspector] private int[] selectedIndices;
        [SerializeField, HideInInspector] private string[] selectedInjectionLayers;

        public SceneReference Scene => scene;
        public AppState State => state;
        public bool UseCustomInjectionLayers => useCustomInjectionLayers;
        public string[] SelectedInjectionLayers => selectedInjectionLayers;

        public void OnValidate()
        {
            for (int i = 0; i < selectedInjectionLayers.Length; i++)
            {
                string s = selectedInjectionLayers[i];

                for (int k = 0; k < selectedInjectionLayers.Length; k++)
                {
                    if (k == i)
                    {
                        continue;
                    }

                    if (s == selectedInjectionLayers[k])
                    {
                        Type type = Type.GetType(s);
                        Log.Error($"Duplicate InjectionLayer <b>{type.Name}</b> found in ApplicationState <b>{name}</b>");
                    }
                }
            }
        }
    }
}
