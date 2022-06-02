using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] bool onMouseClick;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject Brush;
    [SerializeField] GameObject TextEffect;

    void Update()
    {
        MouseInput();
        MouseParticle();
    }
    void onComplete()
    {
        Instantiate(TextEffect, new Vector2(Random.Range(-6f, 6f), -5), Quaternion.identity);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            onComplete();
        }
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
