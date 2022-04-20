using UnityEngine;

namespace MPack
{
    [System.Serializable]
    public class AnimationCurveReference
    {
        public AnimationCurve Value;
        public AnimationCurveVariable Variable;

        public bool UseVariable;
    }
}