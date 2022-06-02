using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    float Rotatedir;
    AllIn1SpriteShaderMaterialInspector allIn1Sprite;
    SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    private void Start()
    {
        Rotatedir = Random.Range(-30, 30);
        spriteRenderer.color = Random.ColorHSV();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Rotatedir) * Time.deltaTime);
        transform.position += new Vector3(0, 2, 0) * Time.deltaTime;
    }
}
