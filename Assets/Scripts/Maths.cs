using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths
{
    public static Vector2 BiNormal(Vector2 u,Vector2 v) 
    {
        Vector2 un = u.normalized;
        Vector2 vn = v.normalized;
        if(un == -vn)
        {
            return Vector2.Perpendicular(un);
        }
        Vector2 c = (un+vn).normalized;
        if(c.x * v.y - v.x * c.y > 0 )
        {
            return (un+vn).normalized;
        }
        else
        {
            return -(un+vn).normalized;
        }
    }
}
