using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawable : MonoBehaviour
{
    public FloreRenderer floreRenderer;
    public new Camera camera;
    protected virtual void OnEnable() 
    {
        /*
        camera = GetComponentInParent<Camera>();
        if(floreRenderer == null)
        {
            //floreRenderer = (FloreRenderer)FindObjectOfType(typeof(FloreRenderer));
            
            floreRenderer = transform.GetComponentInParent<FloreRenderer>();
            
            if(floreRenderer == null)
            {
                floreRenderer = (new GameObject()).AddComponent<FloreRenderer>();
                floreRenderer.gameObject.name = "FloreRenderer";
                floreRenderer.transform.parent = camera.transform;
            }
            
        }*/
        if(floreRenderer != null)
        {
            camera = floreRenderer.transform.GetComponentInChildren<Camera>();
            floreRenderer.AddDrawable(this);
        }
        
    }
    protected virtual void OnDisable() 
    {
        if(floreRenderer != null)
        {
            floreRenderer.RemoveDrawable(this);
        }
    }
}
