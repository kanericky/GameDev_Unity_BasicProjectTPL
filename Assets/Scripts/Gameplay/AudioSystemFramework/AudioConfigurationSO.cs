using UnityEngine;
using UnityEngine.Audio;

namespace Gameplay.AudioSystemFramework
{
    [CreateAssetMenu(menuName = "Audio/Audio Settings")]
    public class AudioConfigurationSO : ScriptableObject
    {
        public AudioMixerGroup OutputAudioMixerGroup;

        [SerializeField] private PriorityLevel _priorityLevel = PriorityLevel.Standard;

        public int Priority
        {
            get => (int) _priorityLevel;
            set => _priorityLevel = (PriorityLevel) value;
        }

        [Header("Sound Properties")] 
        public bool isMute = false;
        [Range(0f, 1f)]   public float volume = 1f;
        [Range(-3f, 3f)]  public float pitch = 1f;
        [Range(-1f, 1f)]  public float panStereo = 0f;
        [Range(0f, 1.1f)] public float reverbZoneMix = 1f;
        
        [Header("Specialisation")]
        [Range(0f, 1f)] public float spatialBlend = 1f;
        public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
        [Range(0.1f, 5f)] public float minDistance = 0.1f;
        [Range(5f, 100f)] public float maxDistance = 50f;
        [Range(0, 360)] public int spread = 0;
        [Range(0f, 5f)] public float dopplerLevel = 1f;

        [Header("Ignores")]
        public bool bypassEffects = false;
        public bool bypassListenerEffects = false;
        public bool bypassReverbZones = false;
        public bool ignoreListenerVolume = false;
        public bool ignoreListenerPause = false;
        
        private enum PriorityLevel
        {
            Highest = 0,
            High = 64,
            Standard = 128,
            Low = 194,
            VeryLow = 256
        }

        public void ApplySettingTo(AudioSource audioSource)
        {
            audioSource.outputAudioMixerGroup = this.OutputAudioMixerGroup;
            audioSource.mute = this.isMute;
            audioSource.bypassEffects = this.bypassEffects;
            audioSource.bypassListenerEffects = this.bypassListenerEffects;
            audioSource.bypassReverbZones = this.bypassReverbZones;
            audioSource.priority = this.Priority;
            audioSource.volume = this.volume;
            audioSource.pitch = this.pitch;
            audioSource.panStereo = this.panStereo;
            audioSource.spatialBlend = this.spatialBlend;
            audioSource.reverbZoneMix = this.reverbZoneMix;
            audioSource.dopplerLevel = this.dopplerLevel;
            audioSource.spread = this.spread;
            audioSource.rolloffMode = this.rolloffMode;
            audioSource.minDistance = this.minDistance;
            audioSource.maxDistance = this.maxDistance;
            audioSource.ignoreListenerVolume = this.ignoreListenerVolume;
            audioSource.ignoreListenerPause = this.ignoreListenerPause;
        }
    }
}
