using System;
using System.Collections.Generic;
using Gameplay.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace Gameplay.AudioSystemFramework
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [Header("Sound Emitters Pool")] 
        [SerializeField] private SoundEmitterFactorySO factory = default;
        [SerializeField] private SoundEmitterPoolSO pool = default;
        [SerializeField] private int initialSize = 10;

        private List<SoundEmitter> _activateSoundEmitters = new List<SoundEmitter>();

        [Header("Audio Channels")] 
        [Tooltip("SFX CHANNEL -The Sound Manager listen to this event")]
        [SerializeField] private AudioCueEventChannelSO sfxEventChannel;
        [Tooltip("MUSIC CHANNEL - The Sound Manager listen to this event")]
        [SerializeField] private AudioCueEventChannelSO musicEventChannel;

        [Header("Audio Control")] 
        [SerializeField] private AudioMixer audioMixer = default;

        [Range(0f, 1f)] [SerializeField] private float masterVolume = 1f;
        [Range(0f, 1f)] [SerializeField] private float sfxVolume = 1f;
        [Range(0f, 1f)] [SerializeField] private float musicVolume = 1f;
        
        // Const string for change volume in different mix groups, DO NOT change unless you know what you are doing
        private const string MASTER_VOLUME = "MasterVolume";
        private const string MUSIC_VOLUME = "MusicVolume";
        private const string SFX_VOLUME = "SFXVolume";

        protected override void Awake()
        {
            base.Awake();
            
            // Subscript to the events
            sfxEventChannel.OnAudioCueRequested += PlayAudioCue;
            musicEventChannel.OnAudioCueRequested += PlayAudioCue;
            
            pool.PreSetup(initialSize);
            pool.SetParent(transform);
            
            SetGroupVolume(MASTER_VOLUME, masterVolume);
            SetGroupVolume(MUSIC_VOLUME, musicVolume);
            SetGroupVolume(SFX_VOLUME, sfxVolume);
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                SetGroupVolume(MASTER_VOLUME, masterVolume);
                SetGroupVolume(MUSIC_VOLUME, musicVolume);
                SetGroupVolume(SFX_VOLUME, sfxVolume);
            }
        }

        public void SetGroupVolume(string parameterName, float normalizedVolume)
        {
            bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
            
            if(!volumeSet)
                Debug.LogError("The Audio Mixer parameter was not found");
        }

        private float NormalizedToMixerValue(float normalizedValue)
        {
            // The range for the volume should be convert to [-80dB to 80dB]
            return (normalizedValue - 1f) * 80f;
        }

        private void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioConfig, Vector3 positionInSpace)
        {
            // Get all the clips needed to be played
            AudioClip[] clipsToPlay = audioCue.GetClips();
            int numOfClips = clipsToPlay.Length;

            // Play the audio clip
            for (int i = 0; i < numOfClips; i++)
            {
                SoundEmitter soundEmitter = pool.Request();
                if (soundEmitter != null)
                {
                    soundEmitter.PlayAudioClip(clipsToPlay[i], audioConfig, audioCue.isLoop, positionInSpace);
                    if (!audioCue.isLoop)
                    {
                        soundEmitter.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
                    }
                }
                else
                {
                    Debug.LogAssertion("1");
                }
            }
        }

        private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
        {
            // UnSubscript event once it has been triggered
            soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
            
            // Stop playing
            soundEmitter.Stop();
            
            // Push the emitter back to pool, to improve performance
            pool.Return(soundEmitter);
        }
    }
}
