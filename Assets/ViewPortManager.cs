using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ViewPortManager : MonoBehaviour
{
    public Camera ownCamera;
    public Camera referenceCamera;
    public Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ownCamera != null && referenceCamera != null)
        {
            Vector2 screenPosition = referenceCamera.WorldToScreenPoint(target);
            Vector2 viewPortPosition = new Vector2(screenPosition.x/Screen.width- ownCamera.rect.width/2,screenPosition.y/Screen.height- ownCamera.rect.height/2);
            Rect rect = new Rect(viewPortPosition.x,viewPortPosition.y,ownCamera.rect.width,ownCamera.rect.height);
            ownCamera.rect = rect;
        }
    }
}
