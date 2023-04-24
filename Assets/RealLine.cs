using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RealLine : MonoBehaviour
{
    public new  Camera camera;
    private Material circle;
    private MaterialPropertyBlock mpb;
    private Mesh quad;
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
    private Material roundedRectangle;
    public Material RoundedRectangle
    {
        get => roundedRectangle;
    }
    public int layer;
    public float currentValue;
    public float previewValue;
    IsInViewPort isInViewPort;
    // Start is called before the first frame update
    void OnEnable()
    {
        line = Resources.Load<Material>("Materials/Solid Color");
        circle = Resources.Load<Material>("Materials/Hollow Circle");
        triangle = Resources.Load<Material>("Materials/Triangle");
        roundedRectangle = Resources.Load<Material>("Materials/RoundedRectangle");;
        quad = MeshUtile.CreateQuad(1,1);
        mpb = new MaterialPropertyBlock();
    }
    void Start()
    {
        isInViewPort = (IsInViewPort)FindObjectOfType(typeof(IsInViewPort));
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 camPosition = camera.transform.position;
        Matrix4x4 m = Matrix4x4.identity;
        Quaternion q = Quaternion.identity;
        float width = 2 * camera.orthographicSize * camera.aspect;
        float halfHeight = camera.orthographicSize;
    

        m.SetTRS(camPosition,q, new Vector3(width,halfHeight * 0.1f,1));
        Graphics.DrawMesh(quad,m, line,layer,null);
        
        q.SetLookRotation(Vector3.forward,Vector2.left);  

        m.SetTRS(Vector3.zero,q, new Vector3(halfHeight,width * 0.005f,1));
        Graphics.DrawMesh(quad,m, line,layer,null);

        float f = Mathf.Floor(Mathf.Log10(width/2))-1; 
        int horizontalCountSmall =  Mathf.CeilToInt(0.5f * width/Mathf.Pow(10,f));
        Vector2Int camPositionInt = Vector2Int.FloorToInt(camPosition);
        
        for(int i = 1;i < horizontalCountSmall ;i++)
        {
            Vector2 pos = new Vector2(-i * Mathf.Pow(10,f),0)+camPositionInt;
            m.SetTRS(pos,q, new Vector3(Mathf.Pow(10,f)* 0.25f,width * 0.005f,1));
            Graphics.DrawMesh(quad,m, line,layer,null);
            pos = new Vector2(i* Mathf.Pow(10,f),0)+camPositionInt;
            m.SetTRS(pos,q, new Vector3(Mathf.Pow(10,f) * 0.25f,width * 0.005f,1));
            Graphics.DrawMesh(quad,m, line,layer,null);
        }
        int horizontalCountBig =  Mathf.CeilToInt(0.5f * width/Mathf.Pow(10,f+1)); 
        
        for(int i = 0;i < horizontalCountBig ;i++)
        {
            Vector2 pos = new Vector2(-i* Mathf.Pow(10,f+1) ,0)+camPositionInt;
            m.SetTRS(pos,q, new Vector3(Mathf.Pow(10,f+1) * 0.25f,width * 0.005f,1));
            Graphics.DrawMesh(quad,m, line,layer,null);
            pos = new Vector2(i* Mathf.Pow(10,f+1),0)+camPositionInt;
            m.SetTRS(pos,q, new Vector3(Mathf.Pow(10,f+1) * 0.25f,width * 0.005f,1));
            Graphics.DrawMesh(quad,m, line,layer,null);
        }
        if(isInViewPort.IsIt(camera))
        {
            previewValue = camera.ScreenToWorldPoint(Input.mousePosition).x;
            q.SetLookRotation(Vector3.forward,Vector3.down);
            m.SetTRS(new Vector2(previewValue,halfHeight/2),q, new Vector3(0.1f * width,0.5f * halfHeight,1));
            
            Graphics.DrawMesh(quad,m, triangle,layer,null,0,mpb);
            if(Input.GetMouseButtonDown(0))
            {
                currentValue = previewValue;
            }
        }

        m.SetTRS(new Vector2(currentValue,0),Quaternion.identity, new Vector3(1,1,1)*halfHeight*0.5f);
        mpb.SetColor("Fill_Color",Color.white);
        mpb.SetFloat("Inner_Radius",0f);
        Graphics.DrawMesh(quad,m, circle,layer,null,0,mpb);
    }
}
