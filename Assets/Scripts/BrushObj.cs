using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushObj : MonoBehaviour, IObjectPoolingObj
{
    public void OnObjCreate()
    {
        foreach (GameObject obj in ObjectPool.Instance.ReturnObjList(gameObject))
        {
            if (obj.transform.position == transform.position && obj != gameObject)
            {
                ObjectPool.Instance.DeleteObj(gameObject);
            }
        }
    }
}
