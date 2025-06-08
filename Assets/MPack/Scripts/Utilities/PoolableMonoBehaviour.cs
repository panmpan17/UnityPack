using UnityEngine;

namespace MPack
{
    public abstract class PoolableMonoBehaviour : MonoBehaviour, IPoolableObj
    {
        public virtual void Instantiate() { }
        public virtual void DeactivateObj(Transform collectionTransform)
        {
            gameObject.SetActive(false);
            if (collectionTransform)
                transform.SetParent(collectionTransform);
        }
        public virtual void Reinstantiate() { gameObject.SetActive(true); }
    }
}