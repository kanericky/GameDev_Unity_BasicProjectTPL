using UnityEngine;

namespace Gameplay.Utilities
{
    public class Singleton<T> : StaticInstance<T> where T: MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            base.Awake();
        }
    }
}
