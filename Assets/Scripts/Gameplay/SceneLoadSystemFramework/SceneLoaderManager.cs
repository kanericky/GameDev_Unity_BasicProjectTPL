using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.SceneLoadSystemFramework.SceneDataStructure.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay.SceneLoadSystemFramework
{
    public class SceneLoaderManager : MonoBehaviour
    {
        [Header("CORE Scene")] 
        [SerializeField] private GameSceneSO coreScene = default;

        [Header("Load on start")] 
        [SerializeField] private GameSceneSO[] mainMenuScenes = default;

        [Header("Loading Screen")] 
        // TODO - Implement the loading screen via UI Toolkit
        [SerializeField] private GameObject loadingInterface;
        [SerializeField] private Image progressBar;

        [Header("Load Event")] 
        // The event channel this manager is listening to
        [SerializeField] private LoadSceneEventChannelSO loadSceneEventChannel;

        // List of scenes to load and track for progress - for progress UI bar
        private List<AsyncOperation> _scenesToLoadAsyncOperations = new List<AsyncOperation>();

        // List of scenes that need to be unloaded
        private List<Scene> _scenesToUnload = new List<Scene>();

        // For the scene that need to be activate, for tracking
        private GameSceneSO _activateScene;


        private void OnEnable()
        {
            // Subscript to load scene events
            loadSceneEventChannel.OnLoadingRequested += LoadScenes;
        }

        private void OnDisable()
        {
            loadSceneEventChannel.OnLoadingRequested -= LoadScenes;
        }

        private void Start()
        {
            // We are currently at the core scene, the core scene severs as a tool modular,
            // which does not have any gameplay ingredients or functionalities
            // Load the menu scene for default
            if (SceneManager.GetActiveScene().name == coreScene.sceneName)
            {
                LoadMainMenu();
            }
        }

        private void LoadMainMenu()
        {
            LoadScenes(mainMenuScenes, false);
        }

        // Load the given scenes
        private void LoadScenes(GameSceneSO[] scenesToLoad, bool showLoadingScreen)
        {
            // Add all the scenes to unload list - will need to unload them later            
            AddScenesToUnload();

            _activateScene = scenesToLoad[0];

            for (int i = 0; i < scenesToLoad.Length; i++)
            {
                string currentSceneName = scenesToLoad[i].sceneName;
                if (!CheckLoadState(currentSceneName))
                {
                    // Add the scene to the list to load asynchronously in backend
                    _scenesToLoadAsyncOperations.Add(SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive));
                }
            }

            _scenesToLoadAsyncOperations[0].completed += SetActiveScene;

            if (showLoadingScreen)
            {
                // TODO - Handle loading scene
                loadingInterface.SetActive(true);
            }
            else
            {
                _scenesToLoadAsyncOperations.Clear();
            }

            UnloadScenes();
        }

        private bool CheckLoadState(String sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddScenesToUnload()
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                
                // Ignore the Core Scene
                if (scene.name != coreScene.sceneName)
                {
                    Debug.Log("Added scene to unload list: " + scene.name);
                    _scenesToUnload.Add(scene);
                }
            }
        }

        private void SetActiveScene(AsyncOperation asyncOp)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_activateScene.sceneName));
        }

        private void UnloadScenes()
        {
            if (_scenesToUnload != null)
            {
                for (int i = 0; i < _scenesToUnload.Count; ++i)
                {
                    SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
                }
            }
            
            _scenesToUnload.Clear();
        }

        private IEnumerator TrackLoadingProgress()
        {
            float totalProgress = 0;

            while (totalProgress <= 0.9f)
            {
                totalProgress = 0;

                for (int i = 0; i < _scenesToLoadAsyncOperations.Count; i++)
                {
                    totalProgress += _scenesToLoadAsyncOperations[i].progress;
                }
                
                // TODO - Integrate the loading scene
                yield return null;
            }
            
            _scenesToLoadAsyncOperations.Clear();
            loadingInterface.SetActive(false);
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game");
            Application.Quit();
        }
        
    }
}
