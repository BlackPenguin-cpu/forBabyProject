using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawManager : MonoBehaviour
{
    bool onMouseClick;
    Image curImage;
    void Update()
    {
        MouseInputManager();
    }
    void MouseInputManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] objs = Physics2D.RaycastAll(Camera.main.WorldToScreenPoint(Input.mousePosition), Vector3.forward);
            foreach (var obj in objs)
            {
                if (obj.transform.CompareTag("StartPoint"))
                {
                    onMouseClick = true;
                    curImage = obj.transform.GetChild(0).GetComponent<Image>();
                }
            }
        }
        if (Input.GetMouseButton(0))
        {

        }
    }
    void nowStart()
    {

    }
}
