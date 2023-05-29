using Gameplay.Factory;
using UnityEngine;

namespace Gameplay.AudioSystemFramework
{
    [CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Audio/SoundEmitter Factory")]
    public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
    {
        public SoundEmitter soundEmitterPrefab = default;
        
        public override SoundEmitter Create()
        {
            return Instantiate(soundEmitterPrefab);
        }
    }
}
