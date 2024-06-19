using UnityEngine;


namespace MPack
{
    [CreateAssetMenu(menuName="MPack/Variable/Range", order=0)]
    public class RangeVariable : ScriptableObject
    {
        public float Min;
        public float Max;

#if UNITY_EDITOR
        [TextArea]
        public string Note;
#endif
    }
}