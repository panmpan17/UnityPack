using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPack;

public class TestAttribute : MonoBehaviour
{
    [SortingLayer]
    public int sortingLayer;
    [Layer]
    public int layer;
    public ValueWithEnable<int> overrideInt;
}
