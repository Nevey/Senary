using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using DI;
using UnityEngine.SceneManagement;
using Utilities;

namespace ApplicationManaging
{
    public static class ApplicationManager
    {
        private static bool isStarted;
        private static ApplicationState currentState;
        private static Dictionary<AppState, ApplicationState> applicationStates = new Dictionary<AppState, ApplicationState>();

        public static ApplicationState CurrentState => currentState;

        /// <summary>
        /// Create required injection layers, if it's not created yet
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void CreateRequiredInjectionLayers()
        {
            if (!currentState.UseCustomInjectionLayers)
            {
                return;
            }

            for (int i = 0; i < currentState.SelectedInjectionLayers.Length; i++)
            {
                Type type = Type.GetType(currentState.SelectedInjectionLayers[i]);

                if (type == null)
                {
                    throw Log.Exception($"ApplicationState {currentState.name} has no eligible InjectionLayer selected!");
                }

                InjectionLayerManager.CreateLayer(type);
            }
        }

        /// <summary>
        /// Opens the required scenes, closes any scenes not required
        /// </summary>
        private static void OpenAndCloseScenes()
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
                if (scenesToClose[i].buildIndex == 0)
                {
                    continue;
                }

                SceneManager.UnloadSceneAsync(scenesToClose[i].buildIndex);
            }
        }

        private static void HandleUI()
        {
            // TODO: Add UI System
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="newState"></param>
        public static void SetState(AppState newState)
        {
            if (!isStarted)
            {
                throw Log.Exception("Cannot set state if not yet started!");
            }

            currentState = applicationStates[newState];

            CreateRequiredInjectionLayers();
            OpenAndCloseScenes();
        }

        public static void RegisterApplicationStates(ApplicationState[] states)
        {
            for (int i = 0; i < states.Length; i++)
            {
                ApplicationState applicationState = states[i];
                applicationStates[applicationState.State] = applicationState;
            }
        }

        /// <summary>
        /// Starts the application manager, creates the default injection layer and sets
        /// the initially given application state
        /// </summary>
        /// <param name="initialState"></param>
        public static void Start(AppState initialState)
        {
            isStarted = true;

            // Create the default injection layer, in case you don't care about DI layering
            InjectionLayerManager.CreateLayer(typeof(InjectionLayer));
            SetState(initialState);
        }
    }
}
