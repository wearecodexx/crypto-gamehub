using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Range
{
    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.max = max;
        this.min = min;
    }
}
