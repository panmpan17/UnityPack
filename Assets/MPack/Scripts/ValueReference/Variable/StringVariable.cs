using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MPack
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "MPack/Variable/String", order=0)]
    public class StringVariable : ScriptableObject
    {
        [TextArea]
        public string Value;
    }
}