using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public float maxOrthographicSize = Mathf.Infinity;
    public float minOrthographicSize = 0;
    public float sensitivity;
    new Camera camera;
    bool moving;
    Vector3 oldMouseScreenPosition;
    public bool lockX;
    public bool lockY;
    IsInViewPort isInViewPort;
    private void OnEnable() 
    {
        camera = GetComponent<Camera>();
    }
    void Start() 
    {
        oldMouseScreenPosition = Input.mousePosition;
        isInViewPort = (IsInViewPort)FindObjectOfType(typeof(IsInViewPort));
    }
    // Update is called once per frame
    void Update()
    {
        if(isInViewPort.IsIt(camera))
        {
            Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            float f = Input.mouseScrollDelta.y;
            
            if(f != 0)
            {
                float oldHeight = camera.orthographicSize ;
                
                camera.orthographicSize -= f * sensitivity * camera.orthographicSize;
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize,minOrthographicSize,maxOrthographicSize);

                float newHeight = camera.orthographicSize ;
                Vector2 camPosition = transform.position;
                float xCorrection = 0;
                float yCorrection = 0;
                if(!lockX)
                {
                    xCorrection = transform.position.x - (mousePosition - (mousePosition-camPosition) * newHeight/oldHeight).x;
                }
                if(!lockY)
                {
                    yCorrection = transform.position.y - (mousePosition - (mousePosition-camPosition) * newHeight/oldHeight).y;
                }
                transform.position -= new Vector3(xCorrection,yCorrection,0);
            }
            if(Input.GetMouseButtonDown(2)) 
            {
                moving = true;
                oldMouseScreenPosition = Input.mousePosition;
            }
        }
        if(Input.GetMouseButtonUp(2)) {moving = false;}
        if(moving)
        {
            Vector2 delta = (Input.mousePosition-oldMouseScreenPosition);
            if(lockX){delta = delta-Vector2.Dot(delta,Vector2.right) * Vector2.right; }
            if(lockY){delta = delta-Vector2.Dot(delta,Vector2.up) * Vector2.up; }
            transform.position -= (Vector3)delta * 2 * (camera.orthographicSize / camera.pixelHeight);
            oldMouseScreenPosition = Input.mousePosition;
        }
        
    }
}
