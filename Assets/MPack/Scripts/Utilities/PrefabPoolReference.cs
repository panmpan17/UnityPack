using MPack;
using UnityEngine;


namespace MPack
{
    [CreateAssetMenu(fileName = "PrefabPoolReference", menuName = "Scriptable Objects/PrefabPoolReference")]
    public class PrefabPoolReference : ScriptableObject
    {
        public PoolableMonoBehaviour Prefab;
        public bool CreatePoolCollection;
        public string PoolCollectionName;

        public PrefabPool<PoolableMonoBehaviour> Pool;

        public void InitializePool()
        {
            if (Pool == null)
            {
                Pool = new PrefabPool<PoolableMonoBehaviour>(
                    Prefab,
                    CreatePoolCollection,
                    PoolCollectionName
                );
            }
        }
    }
}
