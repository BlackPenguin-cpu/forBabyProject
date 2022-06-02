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
        Instantiate(Brush, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }
    void MouseParticle()
    {
        if (!particle.isPlaying && onMouseClick)
            particle.Play();
        else if (particle.isPlaying && !onMouseClick)
            particle.Stop();
        particle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
}
