using Gameplay.Factory;
using Gameplay.Pool;
using UnityEngine;

namespace Gameplay.AudioSystemFramework
{
    [CreateAssetMenu(fileName = "NewSoundEmitterPool", menuName = "Audio/SoundEmitter Pool")]
    public class SoundEmitterPoolSO : ComponentPoolSO<SoundEmitter>
    {
        [SerializeField] private SoundEmitterFactorySO _factory;

        public override IFactory<SoundEmitter> Factory
        {
            get => _factory;
            set => _factory = value as SoundEmitterFactorySO;
        }
    }
}
