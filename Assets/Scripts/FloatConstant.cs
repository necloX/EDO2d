using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class FloatConstant : ConcreteFunction
{
    public float f;
    public RealLine realLine;
    public ViewPortManager viewPortManager;
    bool initialized;
    TMP_Text textMesh;
    void OnEnable()
    {
        SetInput(0);
    }
    public override Type1 Evaluate(Type1 [] inputs,int outputId)
    {
        return new Float(f);
    }
    public override Dot GetInput(int id)
    {
        return node.topDotsInput[id];
    }
    public override Dot GetOutput(int id)
    {
        return node.bottomDotsOutput[id];
    }
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if(node.Selected)
        {
            viewPortManager.gameObject.SetActive(true);
            realLine = viewPortManager.GetComponent<RealLine>();
            if(initialized)
            {
                f = realLine.currentValue;
            }
            else
            {
                realLine.currentValue = f;
                initialized = true;
            }
            int a = Mathf.FloorToInt(Mathf.Abs(f));
            int b = Mathf.FloorToInt(10 * Mathf.Abs(f))-a * 10;
            string sign = "";
            if(f<0) {sign = "-";}
            textMesh.text = sign+a.ToString()+"."+b.ToString();
            viewPortManager.target = transform.position + Vector3.up;
        }
        else
        {
            viewPortManager.gameObject.SetActive(false);
            initialized = false;
        }
    }
}
