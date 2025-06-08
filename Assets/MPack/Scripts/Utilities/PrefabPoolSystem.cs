using MPack;
using UnityEngine;


namespace MPack
{
    public class PrefabPoolSystem : MonoBehaviour
    {
        [SerializeField]
        PrefabPoolReference[] prefabPoolReferences;

        void Awake()
        {
            for (int i = 0; i < prefabPoolReferences.Length; i++)
            {
                prefabPoolReferences[i].InitializePool();
            }
        }
    }
}