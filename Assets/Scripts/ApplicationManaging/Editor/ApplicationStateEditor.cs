using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationManaging;
using DependencyInjection.Layers;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace ApplicationStates.Editor
{
    [CustomEditor(typeof(ApplicationState))]
    public class ApplicationStateEditor : UnityEditor.Editor
    {
        private SerializedProperty selectedIndex;
        private SerializedProperty selectedInjectionLayer;
        
        private string[] injectionLayerTypes;
        private string[] injectionLayerAssemblyQualifiedNames;
        private int injectionLayerIndex;
        
        private void OnEnable()
        {
            // Initialize serialized properties
            selectedIndex = serializedObject.FindProperty("selectedIndex");
            selectedInjectionLayer = serializedObject.FindProperty("selectedInjectionLayer");
            
            // Initialize selectable injection layer array
            List<Type> types = Reflection.GetTypes<InjectionLayer>().ToList();
            List<string> typesToString = new List<string>();
            List<string> assemblyQualifiedNames = new List<string>();

            for (int i = types.Count - 1; i >= 0; i--)
            {
                if (types[i].IsAbstract)
                {
                    continue;
                }
                
                typesToString.Add(types[i].Name);
                assemblyQualifiedNames.Add(types[i].AssemblyQualifiedName);
            }

            injectionLayerTypes = typesToString.ToArray();
            injectionLayerAssemblyQualifiedNames = assemblyQualifiedNames.ToArray();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            serializedObject.Update();
            
            EditorGUILayout.Space();
            
            DrawInjectionLayers();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawInjectionLayers()
        {
            selectedIndex.intValue = EditorGUILayout.Popup(selectedIndex.intValue, injectionLayerTypes, GUILayout.Width(300));
            selectedInjectionLayer.stringValue = injectionLayerAssemblyQualifiedNames[selectedIndex.intValue];
        }
    }
}