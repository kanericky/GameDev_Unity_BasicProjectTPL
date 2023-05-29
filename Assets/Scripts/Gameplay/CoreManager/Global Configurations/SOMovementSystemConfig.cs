using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Core_Manager.Global_Configurations
{
    [CreateAssetMenu(fileName = "Movement System Config", menuName = "Game Config/Movement System Config")]
    public class SOMovementSystemConfig : GameConfigBase
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float accelerateTime;
    }
}
