using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePicker : MonoBehaviour
{
    public FloreRenderer targetRenderer;
    private IsInViewPort isInViewPort;
    private new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        isInViewPort = (IsInViewPort)FindObjectOfType(typeof(IsInViewPort));
        FloreRenderer floreRenderer = transform.GetComponentInParent<FloreRenderer>();
        camera = floreRenderer.transform.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isInViewPort.IsIt(camera))
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = transform.position;
            Vector2 delta = mousePos - pos;
            if( Mathf.Abs(delta.x) < 0.5f && Mathf.Abs(delta.y) < 0.5f)
            {
                GameObject child = transform.GetChild(0).gameObject;
                GameObject childClone = GameObject.Instantiate(child);
                childClone.transform.parent = targetRenderer.transform;
                childClone.SetActive(false);
                Drawable [] drawables = childClone.GetComponentsInChildren<Drawable>();
                foreach (Drawable drawable in drawables)
                {
                    drawable.floreRenderer = targetRenderer;
                    drawable.gameObject.layer = targetRenderer.gameObject.layer;
                }
                childClone.GetComponent<Node>().hold = true;
                childClone.GetComponent<Node>().locked = false;
                childClone.SetActive(true);
            }
        }   
    }
}
