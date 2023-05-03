using System.Collections.Generic;
using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Core_Manager.Global_Configurations
{
    [CreateAssetMenu(fileName = "Game Configs", menuName = "Game Config/Game Config")]
    public class SOGameConfigs : GameConfigBase
    {
        // Data slots
        [SerializeField] public List<GameConfigBase> gameConfigurations;
    }
}
