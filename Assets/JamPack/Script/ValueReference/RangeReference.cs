using UnityEngine;


namespace MPack
{
    [System.Serializable]
    public struct RangeReference
    {
        public float Min;
        public float Max;

        public RangeVariable Variable;

        public bool UseVariable;

        public float PickRandomNumber()
        {
            return UseVariable ? Random.Range(Variable.Min, Variable.Max) : Random.Range(Min, Max);
        }

        public float Clamp(float number)
        {
            return UseVariable ? Mathf.Clamp(number, Variable.Min, Variable.Max) : Mathf.Clamp(number, Min, Max);
        }
    }
}