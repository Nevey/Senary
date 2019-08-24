using System;
using System.Collections.Generic;
using DI;
using UnityEngine.SceneManagement;
using Utilities;

namespace AppManagement
{
    public static class AppManager
    {
        private static AppModeConfig appModeConfig;
        private static bool isStarted;
        private static AppStateConfig currentState;
        private static Dictionary<AppStateEnum, AppStateConfig> applicationStates = new Dictionary<AppStateEnum, AppStateConfig>();

        public static AppStateConfig CurrentState => currentState;

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

            for (int k = 0; k < currentState.Scenes.Length; k++)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);

                    if (currentState.Scenes[k].Name.Equals(scene.name))
                    {
                        newSceneIsAlreadyLoaded = true;
                        continue;
                    }

                    // TODO: Fix not adding scenes that should not close to this list
                    scenesToClose.Add(scene);
                }
            
                if (!newSceneIsAlreadyLoaded)
                {
                    SceneManager.LoadSceneAsync(currentState.Scenes[k]);
                }
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
        /// <param name="newStateEnum"></param>
        public static void SetState(AppStateEnum newStateEnum)
        {
            if (!isStarted)
            {
                throw Log.Exception("Cannot set state if not yet started!");
            }

            currentState = applicationStates[newStateEnum];

            CreateRequiredInjectionLayers();
            OpenAndCloseScenes();
        }

        public static void Setup(AppModeConfig config)
        {
            appModeConfig = config;

            for (int i = 0; i < config.ApplicationStates.Length; i++)
            {
                AppStateConfig applicationState = config.ApplicationStates[i];
                applicationStates[applicationState.StateEnum] = applicationState;
            }
        }

        /// <summary>
        /// Starts the application manager, creates the default injection layer and sets
        /// the initially given application state
        /// </summary>
        public static void Start()
        {
            isStarted = true;

            // Create the default injection layer, in case you don't care about DI layering
            InjectionLayerManager.CreateLayer(typeof(InjectionLayer));
            SetState(appModeConfig.InitialState.StateEnum);
        }
    }
}
