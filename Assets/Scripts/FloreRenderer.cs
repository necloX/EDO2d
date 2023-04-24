using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteAlways]
public class FloreRenderer : MonoBehaviour
{
    public int layerCam;
    [SerializeField]
    private new Camera camera;
    public Camera Camera
    {
        get => camera;
    }
    [SerializeField]
    private ColorSetting colorSetting;
    public ColorSetting ColorSetting
    {
        get => colorSetting;
    }
    [SerializeField]
    private GeometrySettings geometrySettings;
    public GeometrySettings GeometrySettings
    {
        get => geometrySettings;
    }

    [SerializeField]
    private List<Node> nodes;
    [SerializeField]
    private List<Dot> dots;
    [SerializeField]
    private List<Thread> threads;

    private Material roundedRectangle;
    public Material RoundedRectangle
    {
        get => roundedRectangle;
    }
    private Material circle;
    public Material Circle
    {
        get => circle;
    }
    private Material line;
    public Material Line
    {
        get => line;
    }
    private Mesh quad;
    public Mesh Quad
    {
        get => quad;
    }
    private MaterialPropertyBlock mpb;
    public MaterialPropertyBlock Mpb
    {
        get => mpb;
    }
    // Start is called before the first frame update
    private void OnEnable() 
    {
        quad = MeshUtile.CreateQuad(1,1);
        mpb = new MaterialPropertyBlock();
        roundedRectangle =  Resources.Load<Material>("Materials/RoundedRectangle");
        circle =  Resources.Load<Material>("Materials/Hollow Circle");
        line = Resources.Load<Material>("Materials/Line");
        if(nodes == null){nodes = new List<Node>();}
        if(dots == null){dots = new List<Dot>();}
        if(threads == null){threads = new List<Thread>();}
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Node node in nodes)
        {
            DrawNode(node);
        }
        foreach (Dot dot in dots)
        {
            DrawDot(dot);
        }
        foreach (Thread thread in threads)
        {
            DrawThread(thread);
        }
    }
    #region Draw methods
    private void DrawNode(Node node)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS((Vector2)node.transform.position,node.transform.rotation, new Vector3(1,1,1));
        Graphics.DrawMesh(quad,m, roundedRectangle,layerCam,null);
    }
    private void DrawDot(Dot dot)
    {
        if(dot.output)
        {
            DrawOutput((Vector2)dot.transform.position);
        }
        if(dot.input)
        {
            DrawInput((Vector2)dot.transform.position);
        }
    }
    private void DrawInput(Vector2 position)
    {
        PrimitiveDrawer.DrawHollowCircle(position,geometrySettings.dotRadius,0.5f,4,colorSetting.backgroundColor,this);
        PrimitiveDrawer.DrawHollowCircle(position,geometrySettings.dotRadius*0.5f,0,2,colorSetting.nodeColor,this);

    }
    private void DrawOutput(Vector2 position)
    {
        PrimitiveDrawer.DrawHollowCircle(position,geometrySettings.dotRadius,0.5f,2,colorSetting.nodeColor,this);
        PrimitiveDrawer.DrawHollowCircle(position,geometrySettings.dotRadius*0.5f,0,4,colorSetting.backgroundColor,this);
    }
    
    private void DrawThread(Thread thread)
    {
        int n = 100;
        Vector2 [] positions = new Vector2[n];
        for(int k = 0;k<n;k++)
        {
            positions[k] = thread.Evaluate((float)k/(n-1));
        }
        PrimitiveDrawer.DrawLine(positions,geometrySettings,0,colorSetting.typeColor,this);
        float fromSize = geometrySettings.dotRadius * thread.fromScale + geometrySettings.lineThickness * ( 1 -  thread.fromScale);
        float toSize = geometrySettings.dotRadius * thread.toScale + geometrySettings.lineThickness * ( 1 -  thread.toScale);
        PrimitiveDrawer.DrawHollowCircle(thread.Evaluate(0),fromSize,0.5f,1,colorSetting.backgroundColor,this);
        PrimitiveDrawer.DrawHollowCircle(thread.Evaluate(0),fromSize*0.5f,0,4,colorSetting.typeColor,this);
        PrimitiveDrawer.DrawHollowCircle(thread.Evaluate(1),toSize,0.5f,4,colorSetting.typeColor,this);
        PrimitiveDrawer.DrawHollowCircle(thread.Evaluate(1),toSize*0.5f,0,1,colorSetting.backgroundColor,this);
    }
    #endregion Draw methods
    #region Drawable List management
    public void AddDrawable(Drawable drawable)
    {
        if(drawable is Node node)
        {
            if(nodes == null)
            {
                nodes = new List<Node>();
            }
            if(!nodes.Contains(node))
            {   
                nodes.Add(node);
            }
        }
        if(drawable is Dot dot)
        {
            if(dots == null)
            {
                dots = new List<Dot>();
            }
            if(!dots.Contains(dot))
            {   
                dots.Add(dot);
            }
        }
        if(drawable is Thread thread)
        {
            if(threads == null)
            {
                threads = new List<Thread>();
            }
            if(!threads.Contains(thread))
            {
                threads.Add(thread);
            }
        }
    }
    public void RemoveDrawable(Drawable drawable)
    {
        if(drawable is Node node)
        {
            if(nodes != null)
            {
                nodes.Remove(node);
            }
        }
        if(drawable is Dot dot)
        {
            if(dots != null)
            {
                dots.Remove(dot);
            }
        }
        if(drawable is Thread thread)
        {
            if(threads != null)
            {
                threads.Remove(thread);
            }
        }
    }
    [Button("Clear")]
    public void Clear()
    {
        nodes.Clear();
        dots.Clear();
        threads.Clear();
    }
    #endregion Drawable List management
}
