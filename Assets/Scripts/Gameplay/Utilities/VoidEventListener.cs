using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Utilities
{
    
    // A small modular void event listener which can be used in a flexible way...
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO channel = default;

        public UnityEvent OnEventRaised;

        private void OnEnable()
        {
            if (channel != null)
            {
                channel.OnEventRaised += Respond;
            }
        }

        private void OnDisable()
        {
            if (channel != null)
            {
                channel.OnEventRaised -= Respond;
            }
        }

        private void Respond()
        {
            OnEventRaised?.Invoke();
        }
    }
}
