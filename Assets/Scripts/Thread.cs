using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[ExecuteAlways]
public class Thread : Drawable
{
    public Dot dotFrom;
    public Dot dotTo;

    [Range(0,1)]
    public float fromScale = 1;
    [Range(0,1)]
    public float toScale = 1;

    public Curve curve;
    public TimeChange timeChange;

    public Vector2 Evaluate(float t)
    {
        return  curve.Evaluate(t);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        timeChange = new TimeChange(new Vector2(0,1));
        Init();
    }
    [Button("Init")]
    public void Init()
    {
        Transform tf = transform;
        Transform tt = transform;
        if(dotFrom != null) {tf = dotFrom.transform;}
        if(dotTo != null) {tt = dotTo.transform;}
        curve = new Curve(tf,tt);
        curve = Curve.TimeChanged(timeChange,curve);
    }
}
