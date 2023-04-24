using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[ExecuteAlways]
public class Abstraction : ConcreteFunction
{
    Vector2 v;
    void OnEnable()
    {
        node = GetComponent<Node>();
        if(node != null)
        {
            node.topDot = -1;
            node.rightDot = -1;
            node.leftDot = 0;
            node.bottomDot = 1;
        }
    }
    [Button("Compute")]
    public Vector2 Compute(Vector2 input)
    {
        v = input;
        Prod p = inputStream[0].Evaluate(0) as Prod;
        Float x = p.Left() as Float;
        Float y = p.Right() as Float;
        return new Vector2(x.value,y.value);
    }
    public bool IsComplete()
    {
        return true;
    }
    public override Type1 Evaluate(int outputId)
    {
        return new Prod(new Float(v.x),new Float(v.y));
    }
    public override Type1 Evaluate(Type1 [] inputs,int outputId)
    {
        return new Type1();
    }
    public override Dot GetInput(int id)
    {
        return node.bottomDotsInput[id];
    }
    public override Dot GetOutput(int id)
    {
        return node.topDotsOutput[id];
    }
    /*public override Type GetInputType(int id)
    {
        return typeof(Prod);
    }
    public override Type GetOutputType(int id)
    {
        return typeof(Prod);
    }*/
    [Button("SetStream")]
    public override void SetStream()
    {
        if(threads == null)
        {
            threads = new Thread[inputStream.Length];
        }
        for(int k = 0;k<threads.Length;k++)
        {
            UtileObject.SafeDestroyGameObject<Thread>(threads[k]);
        }
        threads = new Thread[inputStream.Length];
        
        for(int k = 0;k<inputStream.Length;k++)
        {
            threads[k] = (new GameObject()).AddComponent<Thread>();
            threads[k].dotFrom = inputStream[k].node.bottomDotsOutput[inputStreamId[k]];
            threads[k].dotTo = node.bottomDotsInput[k];
            threads[k].timeChange.interval = new Vector2(0,1);
            threads[k].Init();
        }
    }
}
