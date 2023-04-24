using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Multiplication : ConcreteFunction
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
            return new Float((f0 as Float).value*(f1 as Float).value);
        }
        if((f0 is Float) && (f1 is Prod))
        {
            Float f = f0 as Float;
            Prod p = f1 as Prod;
            if((p.Left() is Float) && (p.Right() is Float))
            {
                Float pl = p.Left() as Float;
                Float pr = p.Right() as Float;
                Prod po = new Prod(f*pl,f*pr);
                return new Prod(f*pl,f*pr);
            }
        }
        if((f1 is Float) && (f0 is Prod))
        {
            Float f = f1 as Float;
            Prod p = f0 as Prod;
            if((p.Left() is Float) && (p.Right() is Float))
            {
                Float pl = p.Left() as Float;
                Float pr = p.Right() as Float;
                return new Prod(f*pl,f*pr);
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
