using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.AudioSystemFramework
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        // Audio source component
        private AudioSource _audioSource;
        
        public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        /// <summary>
        /// Play the given audio clip
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="audioSetting"></param>
        /// <param name="isLoop"></param>
        /// <param name="position"></param>
        public void PlayAudioClip(AudioClip audioClip, AudioConfigurationSO audioSetting,bool isLoop, Vector3 position = default)
        {
            _audioSource.clip = audioClip;
            audioSetting.ApplySettingTo(_audioSource);
            _audioSource.transform.position = position;
            _audioSource.loop = isLoop;
            _audioSource.Play();

            if (!isLoop)
            {
                StartCoroutine(FinishedPlaying(audioClip.length));
            }
        }

        /// <summary>
        /// Pause the sound
        /// </summary>
        public void Pause()
        {
            if (!_audioSource) return;
            
            _audioSource.Pause();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        /// <summary>
        /// Resume playing the sound
        /// </summary>
        public void Resume()
        {
            if (!_audioSource) return;
            
            _audioSource.Play();
        }
        
        /// <summary>
        /// Sound finish playing callback
        /// </summary>
        /// <param name="clipLength"></param>
        /// <returns></returns>
        IEnumerator FinishedPlaying(float clipLength)
        {
            yield return new WaitForSeconds(clipLength);

            OnSoundFinishedPlaying?.Invoke(this); 
        }
    }
}
