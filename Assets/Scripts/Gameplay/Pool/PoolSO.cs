using System;
using System.Collections.Generic;
using Gameplay.Factory;
using UnityEngine;

namespace Gameplay.Pool
{
    /// <summary>
    /// Base pool generates members of type T on-demand via a factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PoolSO<T> : ScriptableObject, IPool<T>
    {
        protected readonly Stack<T> Available = new Stack<T>();

        public abstract IFactory<T> Factory { get; set; }

        protected bool hasBeenPreSetup = false;
        protected bool HasBeenPreSetup
        {
            get => hasBeenPreSetup; 
            set => hasBeenPreSetup = value; 
        }

        protected virtual T Create()
        {
            return Factory.Create();
        }

        /// <summary>
        /// Pre setup the pool with the given size and type T
        /// </summary>
        /// <param name="poolSize"></param>
        public virtual void PreSetup(int poolSize)
        {
            if (HasBeenPreSetup)
            {
                Debug.LogWarning($"Pool {name} has already been pre setup, please check for duplicated code");
                return;
            }

            for (int i = 0; i < poolSize; i++)
            {
                Available.Push(Create());
            }

            HasBeenPreSetup = true;
        }

        public virtual T Request()
        {
            return Available.Count > 0 ? Available.Pop() : Create();
        }

        /// <summary>
        /// Get requests a type T collection from this pool
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Request(int number = 1)
        {
            List<T> members = new List<T>(number);
            for (int i = 0; i < number; i++)
            {
                members.Add(Request());
            }

            return members;
        }

        /// <summary>
        /// Push the given members back to the pool
        /// </summary>
        /// <param name="member"></param>
        public virtual void Return(T member)
        {
            Available.Push(member);
        }

        // Clear the bool when disabled
        public virtual void OnDisable()
        {
            Available.Clear();
        }
    }
}
