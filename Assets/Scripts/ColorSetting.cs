using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSetting", menuName = "TypeTheory3/ColorSetting", order = 0)]
public class ColorSetting : ScriptableObject 
{
    public Color backgroundColor;
    public Color nodeColor;
    public Color propColor;
    public Color typeColor;    
}
