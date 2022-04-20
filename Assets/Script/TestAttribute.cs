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

    [Space(10)]
    public IntReference intReference;
    public FloatReference floatReference;
    public BoolReference boolReference;

    [Space(10)]
    public ColorReference colorRference;
    public AnimationCurveReference animationCurve;

    [Space(10)]
    public IntRangeReference intRangeReference;
    public RangeReference rangeReference;

    public EventReference eventReference;
}
