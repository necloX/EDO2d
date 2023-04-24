using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUtile
{
    public static Mesh CreateQuad(float width,float height)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(-width/2, -height/2, 0),
            new Vector3(width/2, -height/2, 0),
            new Vector3(-width/2, height/2, 0),
            new Vector3(width/2, height/2, 0)
        };
        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;
        return mesh;
    }
    public static Vector3[] QuadLineVert(Vector2[] positions,GeometrySettings geometrySettings)
    {
        int positionCount = positions.Length;
        if(positionCount<2)
        {
            return null;
        }
        Vector3[] v = new Vector3[positionCount * 2];
        Vector2 bn = Vector2.Perpendicular((positions[1]-positions[0]).normalized);
        v[0] = positions[0] + geometrySettings.lineThickness * bn;
        v[1] = positions[0] - geometrySettings.lineThickness * bn;
        for(int k = 1;k<positionCount-1;k++)
        {
            bn = Maths.BiNormal(positions[k-1]-positions[k],positions[k+1]-positions[k]);
            v[2 * k] = positions[k]-bn * geometrySettings.lineThickness;
            v[2 * k + 1] = positions[k]+bn * geometrySettings.lineThickness;
        }
        bn = Vector2.Perpendicular((positions[positionCount-1]-positions[positionCount-2]).normalized);
        v[2 * positionCount-2] = positions[positionCount-1] + geometrySettings.lineThickness * bn;
        v[2 * positionCount-1] = positions[positionCount-1] - geometrySettings.lineThickness * bn;
        return v;
    }
    public static int[] QuadLineTris(int positionCount)
    {
        int [] t = new int[(positionCount-1) * 6];
        for(int k = 0;k<positionCount-1;k++)
        {
            t[6 * k    ] = 2 * k;
            t[6 * k + 1] = 2 * (k + 1);
            t[6 * k + 2] = 2 * k + 1;
            t[6 * k + 3] = 2 * (k + 1);
            t[6 * k + 4] = 2 * (k + 1) + 1;
            t[6 * k + 5] = 2 * k + 1;
        }
        return t;
    }
    public static Vector2[] QuadLineUV(int positionCount)
    {
        Vector2 [] uv = new Vector2[positionCount * 2];
        for(int k = 0;k<positionCount;k++)
        {
            uv[2 * k    ] = new Vector2(0, k/(float)(positionCount-1));
            uv[2 * k + 1] = new Vector2(1, k/(float)(positionCount-1));
        }
        return uv;
    }
    public static Mesh CreateQuadLineMesh(Vector2[] positions,GeometrySettings geometrySettings)
    {
        
        Mesh mesh = new Mesh();
        if(positions.Length <2)
        {
            return mesh;
        }
        mesh.vertices = QuadLineVert(positions,geometrySettings);
        mesh.triangles = QuadLineTris(positions.Length);
        mesh.uv = QuadLineUV(positions.Length);
        return mesh;
    }
}
