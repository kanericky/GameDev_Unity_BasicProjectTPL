using UnityEngine;

namespace Gameplay.Utilities
{
    public class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour{
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
