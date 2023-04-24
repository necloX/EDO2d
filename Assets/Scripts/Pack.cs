using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Pack : ConcreteFunction
{
    void OnEnable()
    {
        node = GetComponent<Node>();
        if(node != null)
        {
            node.topDot = 2;
            node.rightDot = 0;
            node.leftDot = 0;
            node.bottomDot = -1;
        }
    }
    public override Type1 Evaluate(Type1 [] inputs,int outputId)
    {
        return new Prod(inputs[0],inputs[1]);
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
