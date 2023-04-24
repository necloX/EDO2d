using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VectorField
{
    public Func<Vector2,Vector2> function;

    public Vector2 Evaluate(Vector2 input)
    {
        return function(input);
    }
    public VectorField(Func<Vector2,Vector2> f)
    {
        function = f;
    }
    public static VectorField operator *(float alpha,VectorField vectorField)
    {
        return new VectorField(input => alpha * vectorField.Evaluate(input));
    }
    public static VectorField operator +(VectorField vectorField1,VectorField vectorField2)
    {
        return new VectorField(input => vectorField1.Evaluate(input) + vectorField2.Evaluate(input));
    }
}
public class RealFunction
{
    public Func<float,float> function;

    public float Evaluate(float input)
    {
        return function(input);
    }
    public RealFunction(Func<float,float> f)
    {
        function = f;
    }
    public static RealFunction operator *(float alpha,RealFunction realFunction)
    {
        return new RealFunction(input => alpha * realFunction.Evaluate(input));
    }
    public static RealFunction operator +(RealFunction realFunction1,RealFunction realFunction2)
    {
        return new RealFunction(input => realFunction1.Evaluate(input) + realFunction2.Evaluate(input));
    }
    public static RealFunction operator *(RealFunction realFunction1,RealFunction realFunction2)
    {
        return new RealFunction(input => realFunction1.Evaluate(input) * realFunction2.Evaluate(input));
    }
    public static RealFunction Monom(int n)
    {
        return new RealFunction(input => Mathf.Pow(input,n));
    }
}
public class Matrix
{
    public float a;
    public float b;
    public float c;
    public float d;
    public static Vector2 operator *(Matrix matrix,Vector2 v)
    {
        return new Vector2(matrix.a*v.x + matrix.b*v.y,matrix.c*v.x+matrix.d*v.y);
    }
}