using UnityEngine;

namespace MPack
{
    public class TransformKeep : MonoBehaviour
    {
        [SerializeField]
        private bool keepPosition = true;
        [SerializeField]
        private bool useLocalPosition = false;

        [SerializeField]
        private bool keepRotation = true;
        [SerializeField]
        private bool useLocalRotation = false;
        [SerializeField]
        private bool limitAxis = false;
        [SerializeField]
        private Axis axis = Axis.All;

        private Vector3 _position;
        private Quaternion _rotation;

        void OnEnable()
        {
            if (useLocalPosition)
                _position = transform.localPosition;
            else
                _position = transform.position;


            if (useLocalRotation)
                _rotation = transform.localRotation;
            else
                _rotation = transform.rotation;
        }

        void LateUpdate()
        {
            if (keepPosition)
            {
                if (useLocalPosition)
                    transform.localPosition = _position;
                else
                    transform.position = _position;
            }

            if (keepRotation)
            {
                if (useLocalRotation)
                {
                    Vector3 euler = transform.localEulerAngles;
                    if (limitAxis)
                    {
                        if ((axis & Axis.X) == 0)
                            euler.x = _rotation.eulerAngles.x;
                        if ((axis & Axis.Y) == 0)
                            euler.y = _rotation.eulerAngles.y;
                        if ((axis & Axis.Z) == 0)
                            euler.z = _rotation.eulerAngles.z;
                    }
                    else
                        euler = _rotation.eulerAngles;

                    transform.localEulerAngles = euler;
                }
                else
                {
                    Vector3 euler = transform.eulerAngles;
                    if (limitAxis)
                    {
                        if ((axis & Axis.X) == 0)
                            euler.x = _rotation.eulerAngles.x;
                        if ((axis & Axis.Y) == 0)
                            euler.y = _rotation.eulerAngles.y;
                        if ((axis & Axis.Z) == 0)
                            euler.z = _rotation.eulerAngles.z;
                    }
                    else
                        euler = _rotation.eulerAngles;

                    transform.eulerAngles = euler;
                }
            }
        }


        [System.Flags]
        public enum Axis
        {
            None = 0,
            X = 1,
            Y = 2,
            Z = 4,
            All = X | Y | Z,
        }
    }
}