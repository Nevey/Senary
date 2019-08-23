using System;
using StateMachines;
using UnityEngine;

namespace AppManagement
{
    [Serializable]
    public abstract class AppState : State
    {
        [SerializeField] private ApplicationState applicationState;

        private void OpenAndCloseScenes()
        {
            // List<Scene> scenesToClose = new List<Scene>();

            // bool newSceneIsAlreadyLoaded = false;

            // for (int i = 0; i < SceneManager.sceneCount; i++)
            // {
            //     Scene scene = SceneManager.GetSceneAt(i);

            //     if (currentState.Scene.Name.Equals(scene.name))
            //     {
            //         newSceneIsAlreadyLoaded = true;
            //         continue;
            //     }

            //     scenesToClose.Add(scene);
            // }

            // if (!newSceneIsAlreadyLoaded)
            // {
            //     SceneManager.LoadSceneAsync(currentState.Scene);
            // }

            // for (int i = 0; i < scenesToClose.Count; i++)
            // {
            //     if (scenesToClose[i].buildIndex == 0)
            //     {
            //         continue;
            //     }

            //     SceneManager.UnloadSceneAsync(scenesToClose[i].buildIndex);
            // }
        }
    }
}