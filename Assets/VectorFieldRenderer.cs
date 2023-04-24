using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[ExecuteAlways]
public class VectorFieldRenderer : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    public Camera Camera
    {
        get => camera;
    }
    [SerializeField]
    private float thickness;
    public float Thickness
    {
        get => thickness;
    }
    [SerializeField]
    private float step;
    public float Step
    {
        get => step;
    }
    [SerializeField]
    private float scale;
    public float Scale
    {
        get => scale;
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
    private Material triangle;
    public Material Triangle
    {
        get => triangle;
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

    public Abstraction function;
    // Start is called before the first frame update
    void OnEnable()
    {
        quad = MeshUtile.CreateQuad(1,1);
        line = Resources.Load<Material>("Materials/Solid Color");
        circle = Resources.Load<Material>("Materials/Hollow Circle");
        triangle = Resources.Load<Material>("Materials/Triangle");
        mpb = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        VectorField vectorField = new VectorField( function.Compute);
        float f =  camera.orthographicSize; 
        float r = camera.aspect;
        if(step == 0)
        {
            return;
        }
        int verticalCount = Mathf.CeilToInt( f /step)+2;
        int horizontalCount =  Mathf.CeilToInt( f * r /step)+2;
        int subdivision = horizontalCount / 20;
        Vector2 camPosition = camera.transform.position;
        for(int i = -horizontalCount;i <= horizontalCount ;i = i+1+subdivision)
        {
            for(int j = -verticalCount;j <= verticalCount;j= j+1+subdivision)
            {
                float x = i * step + Mathf.FloorToInt(camPosition.x/step) * step;
                float y = j * step + Mathf.FloorToInt(camPosition.y/step) * step;
                Vector2 position = new Vector2(x,y);
                DrawArrow(position,vectorField.Evaluate(position));
            }
        }
    }
    void DrawArrow(Vector2 position,Vector2 arrow)
    {
        Matrix4x4 m = Matrix4x4.identity;
        Quaternion q = Quaternion.identity;
        q.SetLookRotation(Vector3.forward,arrow);
        float lenght = arrow.magnitude * scale;
        Vector2 center = position +  (Vector2)(q*(new Vector3(0,lenght/2,0)));
        m.SetTRS(center,q, new Vector3(thickness,lenght,1));
        Graphics.DrawMesh(quad,m, line,7,null);
        m.SetTRS(position, Quaternion.identity, new Vector3(thickness,thickness,thickness));
        mpb.SetColor("Fill_Color",Color.white);
        mpb.SetFloat("Inner_Radius",0f);
        Graphics.DrawMesh(quad, m,circle,7,null,0, mpb);
        Vector2 head = position +  (Vector2)(q*(new Vector3(0,lenght,0)));
        m.SetTRS(head, Quaternion.identity, new Vector3(thickness,thickness,thickness));
        Graphics.DrawMesh(quad, m,circle,7,null,0, mpb);
        mpb.SetColor("Color",Color.white);
        m.SetTRS(head, q, new Vector3(thickness,thickness,thickness) * 2);
        Graphics.DrawMesh(quad, m,triangle,7,null,0, mpb);
    }
}
