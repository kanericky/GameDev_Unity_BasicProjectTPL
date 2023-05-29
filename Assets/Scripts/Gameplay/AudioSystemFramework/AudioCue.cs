using UnityEngine;

namespace Gameplay.AudioSystemFramework
{
    public class AudioCue : MonoBehaviour
    {
        [Header("Sound definition")]
        [SerializeField] private AudioCueSO _audioCue = default;
        [SerializeField] private bool _playOnStart = false;

        [Header("Configuration")]
        [SerializeField] private AudioCueEventChannelSO _audioCueEventChannel = default;
        [SerializeField] private AudioConfigurationSO _audioConfiguration = default;

        private void Start()
        {
            if (_playOnStart)
                PlayAudioCue();
        }

        public void PlayAudioCue()
        {
            _audioCueEventChannel.RaiseEvent(_audioCue, _audioConfiguration, transform.position);
        }
    }
}
