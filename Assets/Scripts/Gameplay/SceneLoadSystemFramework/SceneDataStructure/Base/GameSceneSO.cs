using UnityEngine;

namespace Gameplay.SceneLoadSystemFramework.SceneDataStructure.Base
{
    // Base class for all the game scenes
    public class GameSceneSO : ScriptableObject
    {
        [Header("Scene Info")] 
        public string sceneName;
        public string description;

        [Header("Sounds")] 
        public AudioClip music;
    }
}
