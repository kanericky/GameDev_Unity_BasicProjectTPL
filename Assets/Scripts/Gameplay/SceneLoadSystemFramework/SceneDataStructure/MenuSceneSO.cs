using Gameplay.SceneLoadSystemFramework.SceneDataStructure.Base;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

namespace Gameplay.SceneLoadSystemFramework.SceneDataStructure
{

    public enum MenuType
    {
        MainMenu,
        PauseMenu
    }
    
    [CreateAssetMenu(menuName = "Scene Management/Menu " + "Scene Data")]
    public class MenuSceneSO : GameSceneSO
    {
        [Header("Menu Info")] 
        public MenuType menuType;
    }
}
