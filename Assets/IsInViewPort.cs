using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInViewPort : MonoBehaviour
{
    public List<Camera> cameras;
    private Camera current;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int k = cameras.Count;
        bool found = false;
        for (int i = 0; i < k; i++)
        {
            Vector2 inputScreenPosition = new Vector2(Input.mousePosition.x / Screen.width,Input.mousePosition.y / Screen.height);
            Rect rect = cameras[i].rect;
            if(!found && rect.Contains(inputScreenPosition))
            {
                current = cameras[i];
                found = true;
            }
        }
    }
    public bool IsIt(Camera camera)
    {
        return current == camera;
    }
    public void Remove(Camera camera)
    {
        cameras.Remove(camera);
    }
    public void Add(Camera camera)
    {
        if(!cameras.Contains(camera))
        {
            cameras.Add(camera);
        }
    }
}
