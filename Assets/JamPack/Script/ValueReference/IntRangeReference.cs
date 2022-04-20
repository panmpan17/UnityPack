using UnityEngine;


namespace MPack
{
    [System.Serializable]
    public struct IntRangeReference
    {
        public int Min;
        public int Max;

        public IntRangeVariable Variable;

        public bool UseVariable;

        public int PickRandomNumber()
        {
            return UseVariable ? Random.Range(Variable.Min, Variable.Max) : Random.Range(Min, Max);
        }

        public int Clamp(int number)
        {
            return UseVariable ? Mathf.Clamp(number, Variable.Min, Variable.Max) : Mathf.Clamp(number, Min, Max);
        }
    }
}