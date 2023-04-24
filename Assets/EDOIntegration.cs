using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDOIntegration : MonoBehaviour
{
    Rigidbody2D rb;
    public Abstraction vectorField;
    TrailRenderer trail;
    public new  Camera camera;
    private Material circle;
    private MaterialPropertyBlock mpb;
    private Mesh quad;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail = GetComponentInChildren<TrailRenderer>();
        circle = Resources.Load<Material>("Materials/Hollow Circle");
        quad = MeshUtile.CreateQuad(1,1);
        mpb = new MaterialPropertyBlock();
    }
    void Update()
    {
        float f =  camera.orthographicSize; 
        float r = camera.aspect;
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 screenPoint = Input.mousePosition;
            if(screenPoint.x > Screen.width/2)
            {
                transform.position = (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);
                trail.Clear();
            }
        }
        mpb.SetColor("Color",Color.white);
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS(transform.position,  Quaternion.identity, new Vector3(1,1,1) * 0.25f);
        mpb.SetColor("Fill_Color",Color.white);
        mpb.SetFloat("Inner_Radius",0f);
        Graphics.DrawMesh(quad, m,circle,7,null,0, mpb);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (vectorField.Compute((Vector2)transform.position));
    }
}
