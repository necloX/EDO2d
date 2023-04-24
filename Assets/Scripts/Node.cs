using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteAlways]
public class Node : Drawable
{
    public bool locked;

    public int topDot;
    public int bottomDot;
    public int rightDot;
    public int leftDot;
    public GeometrySettings geometrySettings;

    public List<Dot> topDotsInput;
    public List<Dot> topDotsOutput;

    public List<Dot> bottomDotsInput;
    public List<Dot> bottomDotsOutput; 

    public List<Dot> leftDotsInput;
    public List<Dot> leftDotsOutput; 

    public List<Dot> rightDotsInput;
    public List<Dot> rightDotsOutput; 

    public bool hold;
    public bool selected;
    public bool Selected
    {
        get => selected;
    }
    Vector2 holdDelta;
    IsInViewPort isInViewPort;
    [Button("Init")]
    void Init()
    {
        AdjustList(topDotsInput,topDot,true,false);
        AdjustList(topDotsOutput,-topDot,false,true);

        AdjustList(bottomDotsInput,bottomDot,true,false);
        AdjustList(bottomDotsOutput,-bottomDot,false,true);

        AdjustList(leftDotsInput,leftDot,true,false);
        AdjustList(leftDotsOutput,-leftDot,false,true);

        AdjustList(rightDotsInput,rightDot,true,false);
        AdjustList(rightDotsOutput,-rightDot,false,true);

        SpatialInit(topDotsInput,Quaternion.identity,false);
        SpatialInit(topDotsOutput,Quaternion.identity,false);

        SpatialInit(bottomDotsInput,Quaternion.LookRotation(Vector3.forward,Vector3.down),true);
        SpatialInit(bottomDotsOutput,Quaternion.LookRotation(Vector3.forward,Vector3.down),true);

        SpatialInit(leftDotsInput,Quaternion.LookRotation(Vector3.forward,Vector3.left),true);
        SpatialInit(leftDotsOutput,Quaternion.LookRotation(Vector3.forward,Vector3.left),true);

        SpatialInit(rightDotsInput,Quaternion.LookRotation(Vector3.forward,Vector3.right),false);
        SpatialInit(rightDotsOutput,Quaternion.LookRotation(Vector3.forward,Vector3.right),false);
    }
    private void AdjustList(List<Dot> dots,int count,bool input,bool output)
    {
        if(dots == null) 
        {
            dots = new List<Dot>(); 
        }
        if(count > 0)
        {
            while (dots.Count > count)
            {
                UtileObject.SafeDestroyGameObject<Dot>(dots[topDot]);
                dots.RemoveAt(topDot);
            }
            while (dots.Count < count)
            {
                 dots.Add(Dot.CreateDot(this,input,output,dots.Count));
            }
            for(int k = 0;k<dots.Count;k++)
            {
                dots[k].id = k;
            }
        }
        else
        {
            foreach (Dot dot in dots)
            {
                UtileObject.SafeDestroyGameObject<Dot>(dot);
            }
            dots.Clear();
        }
    }
    private void SpatialInit(List<Dot> dots,Quaternion rotation,bool symmetry)
    {
        if(dots != null)
        {
            int n = dots.Count;
            for(int k = 0;k<n;k++)
            {
                float f = 1;
                if(symmetry) {f=-1;}
                Vector2 v = new Vector2(f * (k-0.5f * (n-1)) * geometrySettings.dotSpacing,0.5f);
                dots[k].transform.localPosition = rotation * v;
                if(dots[k].output)
                {
                    dots[k].transform.rotation = rotation;
                }
                else
                {
                    dots[k].transform.rotation = Quaternion.LookRotation(Vector3.forward,Vector3.down) * rotation;
                }
            }
        }
        
    }
    void Start()
    {
        isInViewPort = (IsInViewPort)FindObjectOfType(typeof(IsInViewPort));
    }
    void Update() 
    {
        Dot highlitedDot = null;
        if(isInViewPort.IsIt(camera))
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(topDotsInput,mousePos);}
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(topDotsOutput,mousePos);}
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(rightDotsInput,mousePos);}
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(rightDotsOutput,mousePos);}
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(leftDotsInput,mousePos);}
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(leftDotsOutput,mousePos);}
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(bottomDotsInput,mousePos);}
            if(highlitedDot == null) {highlitedDot = CheckDotsInput(bottomDotsOutput,mousePos);}
        }
        if(Input.GetMouseButtonDown(0) && isInViewPort.IsIt(camera))
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            UpdateDots(topDotsInput,highlitedDot);
            UpdateDots(topDotsOutput,highlitedDot);
            UpdateDots(rightDotsInput,highlitedDot);
            UpdateDots(rightDotsOutput,highlitedDot);
            UpdateDots(leftDotsInput,highlitedDot);
            UpdateDots(leftDotsOutput,highlitedDot);
            UpdateDots(bottomDotsInput,highlitedDot);
            UpdateDots(bottomDotsOutput,highlitedDot);
            
            Vector2 pos = transform.position;
            Vector2 delta = mousePos - pos;
            if( Mathf.Abs(delta.x) < 0.5f && Mathf.Abs(delta.y) < 0.5f && highlitedDot == null)
            {
                hold = true;
                holdDelta = delta;
                selected = true;
            }
            else
            {
                selected = false;
            }
        }
        if(hold && !locked)
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos-holdDelta;
        }
        if(Input.GetMouseButtonUp(0))
        {
            UnHoldDot(topDotsInput);
            UnHoldDot(topDotsOutput);
            UnHoldDot(rightDotsInput);
            UnHoldDot(rightDotsOutput);
            UnHoldDot(leftDotsInput);
            UnHoldDot(leftDotsOutput);
            UnHoldDot(bottomDotsInput);
            UnHoldDot(bottomDotsOutput);
            hold = false;
        }
    }
    Dot CheckDotsInput(List<Dot> dots,Vector2 mousePos)
    {
        Dot highlitedDot = null;
        foreach (Dot dot in dots)
        {
            Vector2 dotPos = dot.transform.position;
            Vector2 delta = mousePos - dotPos;
            if( delta.magnitude < geometrySettings.dotRadius && highlitedDot == null)
            {
                highlitedDot = dot;
                dot.highlight = true;
            }
            else
            {
                dot.highlight = false;
            }
        }
        return highlitedDot;
    }
    void UpdateDots(List<Dot> dots,Dot highlitedDot)
    {
        foreach (Dot dot in dots)
        {
            if(dot == highlitedDot)
            {
                dot.hold = true;
                dot.selected = true;
            }
            else
            {
                dot.selected = false;
            }
        }
    }
    void UnHoldDot(List<Dot> dots)
    {
        foreach (Dot dot in dots)
        {
            dot.hold = false;
            dot.threadInit = false;
        }
    }
}
