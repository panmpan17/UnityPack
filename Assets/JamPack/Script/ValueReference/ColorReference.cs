using UnityEngine;

namespace MPack
{
    [System.Serializable]
    public class ColorReference
    {
        public Color Value;
        public ColorVariable Variable;

        public bool UseVariable;
    }
}