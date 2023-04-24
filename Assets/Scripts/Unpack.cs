using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Unpack : ConcreteFunction
{
    void OnEnable()
    {
        node = GetComponent<Node>();
        if(node != null)
        {
            node.topDot = 1;
            node.rightDot = 0;
            node.leftDot = 0;
            node.bottomDot = -2;
        }
    }
    public override Type1 Evaluate(Type1 [] inputs,int outputId)
    {
        if(inputs[0] is Prod)
        {
            Prod p = inputs[0] as Prod;
            if(outputId == 0)
            {
                return p.Left();
            }
            else
            {
                return p.Right();
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
