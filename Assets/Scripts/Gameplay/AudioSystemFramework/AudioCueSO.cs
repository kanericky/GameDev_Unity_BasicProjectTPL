using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.AudioSystemFramework
{
    [CreateAssetMenu(fileName = "New Audio Cue", menuName = "Audio/Audio Cue")]
    public class AudioCueSO : ScriptableObject
    {
        public bool isLoop = false;

        [SerializeField] private AudioClipsGroup[] _audioClipGroups = default;

        public AudioClip[] GetClips()
        {
            int numberOfClips = _audioClipGroups.Length;
            AudioClip[] resultClips = new AudioClip[numberOfClips];

            for (int i = 0; i < numberOfClips; i++)
            {
                resultClips[i] = _audioClipGroups[i].GetNextClip();
            }

            return resultClips;
        }
    }

    [Serializable]
    public class AudioClipsGroup
    {
        public SequenceMode sequenceMode = SequenceMode.Sequential;
        public AudioClip[] audioClips;

        private int _nextClipToPlayIndex = -1;
        private int _lastClipPlayedIndex = -1;

        public AudioClip GetNextClip()
        {
            if (audioClips.Length == 1) return audioClips[0];

            // Init the index for the next SFX needed play
            if (_nextClipToPlayIndex == -1)
            {
                _nextClipToPlayIndex =
                    (sequenceMode == SequenceMode.Sequential) ? 0 : Random.Range(0, audioClips.Length);
            }
            else
            {
                // Select next audio clip based on the sequence mode
                switch (sequenceMode)
                {
                    case SequenceMode.Random:
                        _nextClipToPlayIndex = Random.Range(0, audioClips.Length);
                        break;
                    
                    case SequenceMode.RandomNoImmediateRepeat:
                        do { _nextClipToPlayIndex = Random.Range(0, audioClips.Length); }
                        while (_nextClipToPlayIndex == _lastClipPlayedIndex);
                        break;
                    
                    case SequenceMode.Sequential:
                        _nextClipToPlayIndex = (int) Mathf.Repeat(++_nextClipToPlayIndex, audioClips.Length);
                        break;
                        ;
                }
            }

            _lastClipPlayedIndex = _nextClipToPlayIndex;

            return audioClips[_nextClipToPlayIndex];
        }
    }


    public enum SequenceMode
    {
        Random,
        RandomNoImmediateRepeat,
        Sequential,
    }
}
