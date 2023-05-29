using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.AudioSystemFramework
{
    [CreateAssetMenu(menuName = "Audio/Audio Cue Event Channel")]
    public class AudioCueEventChannelSO : ScriptableObject
    {
        public UnityAction<AudioCueSO, AudioConfigurationSO, Vector3> OnAudioCueRequested;

        public void RaiseEvent(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace)
        {
            if (OnAudioCueRequested != null)
            {
                OnAudioCueRequested.Invoke(audioCue, audioConfiguration, positionInSpace);
            }
            else
            {
                Debug.LogWarning("An audio cue was requested, but nothing picked it up." + 
                                 "Potential issue - NO AUDIO MANAGER" + 
                                 "Potential issue - AUDIO MANAGER HAS NOT SUBSCRIBED THE EVENT");
            } 
        }
    
    }
}
