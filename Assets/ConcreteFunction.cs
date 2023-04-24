using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public abstract class ConcreteFunction : MonoBehaviour
{
    public ConcreteFunction [] inputStream;
    public Thread [] threads;
    public int [] inputStreamId;
    public Node node;
    public virtual Type1 Evaluate(int outputId)
    {
        Type1 [] inputs = new Type1[inputStream.Length];
        for(int k = 0;k<inputStream.Length;k++)
        {
            inputs[k] = inputStream[k].Evaluate(inputStreamId[k]);
        }
        return Evaluate(inputs,outputId);
    }
    public abstract Type1 Evaluate(Type1 [] inputs,int outputId);
    public abstract Dot GetInput(int id);
    public abstract Dot GetOutput(int id);
    //public abstract Type GetInputType(int id);
    //public abstract Type GetOutputType(int id);
    /*
    public static bool TypeCheck(ConcreteFunction cf1,int id1,ConcreteFunction cf2,int id2)
    {
        return cf1.GetOutputType(id1) == cf2.GetOutputType(id2);
    }*/
    public void SetInput(int number)
    {
        node = GetComponent<Node>();
        if(node != null)
        {
            node.topDot = number;
            node.rightDot = 0;
            node.leftDot = 0;
            node.bottomDot = -1;
        }
    }
    [Button("SetStream")]
    public virtual void SetStream()
    {
        for(int k = 0;k<threads.Length;k++)
        {
            UtileObject.SafeDestroyGameObject<Thread>(threads[k]);
        }
        threads = new Thread[inputStream.Length];
        
        for(int k = 0;k<inputStream.Length;k++)
        {
            threads[k] = (new GameObject()).AddComponent<Thread>();
            threads[k].gameObject.SetActive(false);
            threads[k].floreRenderer = node.floreRenderer;
            threads[k].dotFrom = inputStream[k].GetOutput(inputStreamId[k]);
            threads[k].dotTo = GetInput(k);
            threads[k].timeChange.interval = new Vector2(0,1);
            threads[k].gameObject.SetActive(true);
        }
    }
}
public class Type1
{

}
public class Float:Type1
{
    public float value;
    public Float()
    {
        value = 0;
    }
    public Float(float f)
    {
        value = f;
    }
    public static Float operator +(Float a,Float b)
    {
        return new Float(a.value + b.value);
    }
    public static Float operator *(Float a,Float b)
    {
        return new Float(a.value * b.value);
    }
}
public class Prod:Type1
{
    public Type1 left;
    public Type1 right;

    public Prod(Type1 l,Type1 r) 
    {
        left = l;
        right = r;
    }
    public Type1 Left() 
    {
        return left;
    }
    public Type1 Right() 
    {
        return right;
    }
    public static Prod operator *(Float a,Prod b)
    {
        Float x = b.Left() as Float;
        Float y = b.Right() as Float;
        return new Prod(a * x,a * y);
    }
}
public class Sum:Type1
{
    public Type1 left;
    public Type1 right;

    public Sum(Type1 l) 
    {
        left = l;
    }
}