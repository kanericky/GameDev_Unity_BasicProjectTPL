using Gameplay.SceneLoadSystemFramework.SceneDataStructure.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.SceneLoadSystemFramework
{
    [CreateAssetMenu(menuName = "Scene Management/Load Scene Event Channel")]
    public class LoadSceneEventChannelSO : ScriptableObject
    {
        public UnityAction<GameSceneSO[], bool> OnLoadingRequested;

        public void RaiseEvent(GameSceneSO[] scenesToLoad, bool showLoadingScreen)
        {
            if (OnLoadingRequested != null)
            {
                OnLoadingRequested.Invoke(scenesToLoad, showLoadingScreen);
            }
            else
            {
                Debug.LogWarning("A request for loading new scene has been raise, but there is no subscriber" + 
                                 "Potential Issue - There is no Scene Loader Manager exist" + 
                                 "Potential Issue - The Scene Loader Manager has not listened to this event");
            }
        }
    }
}
