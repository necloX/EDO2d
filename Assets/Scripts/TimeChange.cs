using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
[Serializable]
public class TimeChange
{
    [MinMaxSlider(0,1)]
    public Vector2 interval;
    public float Evaluate(float t)
    {
        return t * (interval.y-interval.x) + interval.x;
    }
    public TimeChange(Vector2 i)
    {
        interval = i;
    }
}
