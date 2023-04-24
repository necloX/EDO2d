using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : Drawable
{
    public bool locked;
    public bool input;
    public bool output;
    public bool threadInit;
    public Node node;
    public int id;
    public IsInViewPort isInViewPort;
    public bool hold;
    public bool selected;
    public bool Selected
    {
        get => selected;
    }
    public bool highlight;
    public static Dot CreateDot(Node n,bool inp,bool outp,int id)
    {
        Dot dot = (new GameObject()).AddComponent<Dot>();
        dot.transform.parent = n.transform;
        dot.node = n;
        dot.input = inp;
        dot.output = outp;
        dot.id = id;
        return dot;
    }
    void Start()
    {
        isInViewPort = (IsInViewPort)FindObjectOfType(typeof(IsInViewPort));
    }
    void Update() 
    {
        if(hold && !locked)
        {
            if(!threadInit)
            {
                threadInit = true;
                GameObject threadObject = new GameObject();
                Thread thread = threadObject.AddComponent<Thread>();
                thread.floreRenderer = floreRenderer;
                thread.camera = camera;
                threadObject.SetActive(false);
                if(input)
                {
                    thread.dotTo = this;
                    thread.dotFrom = MasterDot.instance;
                    
                }
                else
                {
                    thread.dotFrom = this;
                    thread.dotTo = MasterDot.instance;
                }
                MasterDot.instance.attachedThread = thread;
                MasterDot.instance.notLooseEnd = this;
                threadObject.SetActive(true);
            }
        }
        if(highlight)
        {
            if(MasterDot.instance.attachedThread != null)
            {
                if(MasterDot.instance.attachedThread.dotFrom == MasterDot.instance && output)
                {
                    MasterDot.instance.attachedThread.dotFrom = this;
                    MasterDot.instance.attachedThread.Init();
                }
                if(MasterDot.instance.attachedThread.dotTo == MasterDot.instance && input)
                {
                    MasterDot.instance.attachedThread.dotTo = this;
                    MasterDot.instance.attachedThread.Init();
                }
            }
        }
        else
        {
            if(MasterDot.instance.attachedThread != null)
            {
                if(MasterDot.instance.notLooseEnd != this)
                {
                    if(MasterDot.instance.attachedThread.dotFrom == this)
                    {
                        MasterDot.instance.attachedThread.dotFrom = MasterDot.instance;
                        MasterDot.instance.attachedThread.Init();
                    }
                    if(MasterDot.instance.attachedThread.dotTo == this)
                    {
                        MasterDot.instance.attachedThread.dotTo = MasterDot.instance;
                        MasterDot.instance.attachedThread.Init();
                    }
                }
                
            }
        }
    }
}
