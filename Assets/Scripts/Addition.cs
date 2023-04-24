using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Addition : ConcreteFunction
{
    void OnEnable()
    {
        SetInput(2);
    }
    public override Type1 Evaluate(Type1 [] inputs,int outputId)
    {
        Type1 f0 = inputs[0];
        Type1 f1 = inputs[1];
        if((f0 is Float) && (f1 is Float))
        {
            Float ff0 = f0 as Float;
            Float ff1 = f1 as Float;
            return ff0 + ff1;
        }
        if((f0 is Prod) && (f1 is Prod))
        {
            Prod p0 = f0 as Prod;
            Prod p1 = f1 as Prod;
            if((p0.Left() is Float) && (p0.Right() is Float) && (p1.Right() is Float) && (p1.Right() is Float))
            {
                Float p0l = p0.Left() as Float;
                Float p0r = p0.Right() as Float;
                Float p1l = p1.Left() as Float;
                Float p1r = p1.Right() as Float;
                return new Prod(p0l+p1l,p0r+p1r);
            }
        }
        return new Type1();
    }
    public override Dot GetInput(int id)
    {
        return node.topDotsInput[id];
    }
    public override Dot GetOutput(int id)
    {
        return node.bottomDotsOutput[id];
    }
}
