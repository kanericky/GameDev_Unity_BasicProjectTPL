using UnityEngine;

namespace Gameplay.Factory
{
    /// <summary>
    /// Base factory class for creating data configurations of factories based on types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FactorySO<T> : ScriptableObject, IFactory<T>
    {
        public abstract T Create();
    }
}
