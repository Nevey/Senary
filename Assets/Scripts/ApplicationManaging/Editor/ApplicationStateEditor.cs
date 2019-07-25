using System;
using System.Collections.Generic;
using System.Linq;
using DI;
using Paladin.Framework.Extensions;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace ApplicationManaging.Editor
{
    [CustomEditor(typeof(ApplicationState))]
    public class ApplicationStateEditor : UnityEditor.Editor
    {
        private SerializedProperty selectedIndices;
        private SerializedProperty useCustomInjectionLayers;
        private SerializedProperty selectedInjectionLayers;
        
        private string[] injectionLayerTypes;
        private string[] injectionLayerAssemblyQualifiedNames;
        private int injectionLayerIndex;
        
        private void OnEnable()
        {
            // Initialize serialized properties
            selectedIndices = serializedObject.FindProperty("selectedIndices");
            useCustomInjectionLayers = serializedObject.FindProperty("useCustomInjectionLayers");
            selectedInjectionLayers = serializedObject.FindProperty("selectedInjectionLayers");
            
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
            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.PropertyField(useCustomInjectionLayers);
            
            if (!useCustomInjectionLayers.boolValue)
            {
                EditorGUILayout.EndVertical();
                return;
            }

            EditorGUILayout.BeginVertical("Box");

            for (int i = 0; i < selectedIndices.arraySize; i++)
            {
                EditorGUILayout.BeginVertical("Box");
                
                int index = selectedIndices.GetArrayElementAtIndex(i).intValue;
                index = EditorGUILayout.Popup(index, injectionLayerTypes);
                selectedIndices.GetArrayElementAtIndex(i).intValue = index;

                string layer = selectedInjectionLayers.GetArrayElementAtIndex(i).stringValue;
                layer = injectionLayerAssemblyQualifiedNames[index];
                selectedInjectionLayers.GetArrayElementAtIndex(i).stringValue = layer;

                if (GUILayout.Button("Remove"))
                {
                    selectedIndices.DeleteArrayElementAtIndex(i);
                    selectedInjectionLayers.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndVertical();
                
                EditorGUILayout.Separator();
            }
            
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal("Box");
            
            if (GUILayout.Button("Add"))
            {
                selectedIndices.AddArrayElement();
                selectedInjectionLayers.AddArrayElement();
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndVertical();
        }
    }
}