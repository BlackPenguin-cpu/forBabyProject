using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] bool onMouseClick;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject Brush;

    void Update()
    {
        MouseInput();
        MouseParticle();
        if (onMouseClick)
        {
            onBrushing();
        }
    }
    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onMouseClick = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            onMouseClick = false;
        }
    }
    void onBrushing()
    {
        Instantiate(Brush, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10, Quaternion.identity);
    }
    void MouseParticle()
    {
        if (!particle.isEmitting && onMouseClick)
            particle.Play();
        else if (particle.isEmitting && !onMouseClick)
            particle.Stop();
        particle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
}
