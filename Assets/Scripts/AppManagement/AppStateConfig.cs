using System;
using UnityEngine;
using Utilities;

namespace AppManagement
{
    [CreateAssetMenu(fileName = "AppStateConfig", menuName = "AppManagement/AppStateConfig")]
    public class AppStateConfig : ScriptableObject
    {
        [SerializeField] private SceneReference[] scenes;
        [SerializeField] private AppStateEnum stateEnum;
        [SerializeField, HideInInspector] private bool useCustomInjectionLayers;
        [SerializeField, HideInInspector] private int[] selectedIndices;
        [SerializeField, HideInInspector] private string[] selectedInjectionLayers;

        public SceneReference[] Scenes => scenes;
        public AppStateEnum StateEnum => stateEnum;
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
                        Log.Error($"Duplicate InjectionLayer <b>{type.Name}</b> found in AppStateConfig <b>{name}</b>");
                    }
                }
            }
        }
    }
}
