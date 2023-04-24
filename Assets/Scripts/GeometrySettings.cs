using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeometrySettings", menuName = "TypeTheory3/GeometrySettings", order = 0)]
public class GeometrySettings : ScriptableObject 
{
    [Range(0,0.2f)]
    public float lineThickness;
    [Range(0,1)]
    public float layerThickness;
    [Range(0,0.5f)]
    public float dotRadius;
    [Range(0,1)]
    public float dotSpacing;
}
