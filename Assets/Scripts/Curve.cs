using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
[Serializable]
public class Curve
{
    public Func<float,Vector2> function;
    public static Curve zero = new Curve();
    public Curve()
    {
        function = (t => Vector2.zero);
    }
    public Curve(Func<float,Vector2> f)
    {
        function = f;
    }
    public Curve(Transform from,Transform to)
    {
        function = (t =>
        {
            Curve c = new Curve(from.position,to.position,from.up,to.up);

            return c.Evaluate(t);
        })
        ;
    }
    public Curve(Vector2 from,Vector2 to,Vector2 fromDirection,Vector2 toDirection)
    {
        function = (t =>
        {
            float s = (1-t);
            float s2 = s*s;
            float t2 = t*t;
            float s3 = s*s*s;
            float t3 = t*t*t;

            return from * (s+3*t) * s2 + s2 * t * 2 * fromDirection - s * t2 * 2 * toDirection + to * t2 * (t+3 * s);
        })
        ;
    }
    [Button("Evaluate")]
    public Vector2 Evaluate(float t)
    {
        return function(t);
    }
    public static Curve TimeChanged(TimeChange timeChange,Curve curve)
    {
        return new Curve(t=> curve.Evaluate(timeChange.Evaluate(t)));
    }
    public static Curve PreCompose(Func<float,float> timeChange,Curve curve)
    {
        return new Curve(t => curve.Evaluate(timeChange(t)));
    }
    public static Curve operator *(float alpha,Curve curve)
    {
        return new Curve(t => alpha * curve.Evaluate(t));
    }
    public static Curve operator +(Curve curve1,Curve curve2)
    {
        return new Curve(t => curve1.Evaluate(t) + curve2.Evaluate(t));
    }
}

