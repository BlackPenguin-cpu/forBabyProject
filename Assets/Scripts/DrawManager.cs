using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] bool onMouseClick;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject Brush;
    [SerializeField] GameObject TextEffect;

    private Camera mainCamera;
    private float curTime;

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
            curTime += Time.deltaTime;
        }
        if (curTime > 0.02f)
        {
            onBrushing();
            curTime = 0;
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
        if (OnChecking())
            ObjectPool.Instance.CreateObj(Brush, mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10, Quaternion.identity);
    }
    bool OnChecking()
    {
        RaycastHit2D[] rays = Physics2D.RaycastAll(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        foreach (RaycastHit2D ray in rays)
        {
            if (ray.transform.tag == "Drawable")
            {
                return true;
            }
        }
        return false;
    }
    private void Start()
    {
        mainCamera = Camera.main;
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
