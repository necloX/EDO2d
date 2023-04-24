using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDot : Dot
{
    public static MasterDot instance;
    public Dot notLooseEnd;
    public Thread attachedThread;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                UtileObject.SafeDestroy<MasterDot>(instance);
                instance = this;
            }
        }
        isInViewPort = (IsInViewPort)FindObjectOfType(typeof(IsInViewPort));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && attachedThread != null)
        {
            if(attachedThread.dotTo != this && attachedThread.dotFrom != this)
            {
                attachedThread.dotTo.node.GetComponent<ConcreteFunction>().inputStream[attachedThread.dotTo.id] = attachedThread.dotFrom.node.GetComponent<ConcreteFunction>();
                attachedThread.dotTo.node.GetComponent<ConcreteFunction>().inputStreamId[attachedThread.dotTo.id] = attachedThread.dotFrom.id;
                attachedThread.dotTo.node.GetComponent<ConcreteFunction>().SetStream();
            }
            UtileObject.SafeDestroyGameObject<Thread>(attachedThread);
        }
        if(camera != null)
        {
            if(isInViewPort.IsIt(camera))
            {
                transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}
