using System;
using System.Collections.Generic;
using DependencyInjection.Layers;
using UnityEngine.SceneManagement;
using Utilities;

namespace ApplicationManaging
{
    public static class ApplicationManager
    {
        private static Dictionary<Type, InjectionLayer> injectionLayers = new Dictionary<Type, InjectionLayer>();

        private static ApplicationState previousState;
        private static ApplicationState currentState;

        private static void HandleInjectionLayers()
        {
            Type type = Type.GetType(currentState.SelectedInjectionLayer);

            if (type == null)
            {
                throw Log.Exception($"ApplicationState {currentState.name} has no eligible InjectionLayer selected!");
            }

            if (!injectionLayers.ContainsKey(type))
            {
                injectionLayers[type] = (InjectionLayer)Activator.CreateInstance(type);
            }
        }
        
        private static void HandleScenes()
        {
            List<Scene> scenesToClose = new List<Scene>();

            bool newSceneIsAlreadyLoaded = false;

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (currentState.Scene.Name.Equals(scene.name))
                {
                    newSceneIsAlreadyLoaded = true;
                    continue;
                }

                scenesToClose.Add(scene);
            }

            if (!newSceneIsAlreadyLoaded)
            {
                SceneManager.LoadSceneAsync(currentState.Scene);
            }

            for (int i = 0; i < scenesToClose.Count; i++)
            {
                SceneManager.UnloadSceneAsync(scenesToClose[i].buildIndex);
            }
        }

        private static void HandleUI()
        {
            // TODO: Add UI System
        }

        public static void SetState(ApplicationState newState)
        {
            previousState = currentState;
            currentState = newState;
            
            HandleInjectionLayers();
            HandleScenes();
        }
    }
}