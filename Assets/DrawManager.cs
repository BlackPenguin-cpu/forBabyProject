using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawManager : MonoBehaviour
{
    [SerializeField] bool onMouseClick;
    [SerializeField] ParticleSystem particle;
    Image curImage;
    void Update()
    {
        MouseInputManager();
        MouseParticle();
    }
    void MouseParticle()
    {
        if (!particle.isPlaying && onMouseClick)
            particle.Play();
        else if (particle.isPlaying && !onMouseClick)
            particle.Stop();
        particle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
    void MouseInputManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onMouseClick = true;
            RaycastHit2D[] objs = Physics2D.RaycastAll(Camera.main.WorldToScreenPoint(Input.mousePosition), Vector3.forward);
            foreach (var obj in objs)
            {
                if (obj.transform.CompareTag("StartPoint"))
                {
                    curImage = obj.transform.GetChild(0).GetComponent<Image>();
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && onMouseClick)
        {
            RaycastHit2D[] objs = Physics2D.RaycastAll(Camera.main.WorldToScreenPoint(Input.mousePosition), Vector3.forward);
            foreach (var obj in objs)
            {
                if (obj.transform.CompareTag("EndPoint"))
                {
                    curImage = obj.transform.GetChild(0).GetComponent<Image>();
                }
            }
            onMouseClick = false;
        }
    }
    void nowStart()
    {

    }
}
