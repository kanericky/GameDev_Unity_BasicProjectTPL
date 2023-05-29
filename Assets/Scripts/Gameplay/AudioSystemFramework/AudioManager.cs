using Gameplay.Utilities;
using UnityEngine;

namespace Gameplay.AudioSystemFramework
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [Header("Sound Emitters Pool")] 
        [SerializeField] private SoundEmitterFactorySO factory = default;
        [SerializeField] private int initialSize = 10;
    }
}
