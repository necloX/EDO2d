using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitiveDrawer
{
    public static void DrawHollowCircle(Vector2 position,float radius,float innerRadiusRatio,int layer,Color color,FloreRenderer floreRenderer)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS((Vector3)position-layer * floreRenderer.GeometrySettings.layerThickness * Vector3.forward, Quaternion.identity, new Vector3(radius,radius,radius) * 2);
        floreRenderer.Mpb.SetColor("Fill_Color",color);
        floreRenderer.Mpb.SetFloat("Inner_Radius",innerRadiusRatio);
        Graphics.DrawMesh(floreRenderer.Quad, m,floreRenderer.Circle,floreRenderer.layerCam,null,0, floreRenderer.Mpb);
    }
    public static void DrawLine(Vector2 [] positions,GeometrySettings geometrySettings,int layer,Color color,FloreRenderer floreRenderer)
    {
        Mesh lineMesh = MeshUtile.CreateQuadLineMesh(positions,floreRenderer.GeometrySettings);
        Matrix4x4 m = Matrix4x4.identity;
        m.SetTRS(-layer *floreRenderer.GeometrySettings.layerThickness * Vector3.forward, Quaternion.identity, Vector3.one);
        floreRenderer.Mpb.SetColor("_Color",color);
        Graphics.DrawMesh(lineMesh, m,floreRenderer.Line,floreRenderer.layerCam,null,0, floreRenderer.Mpb);
    }
}
