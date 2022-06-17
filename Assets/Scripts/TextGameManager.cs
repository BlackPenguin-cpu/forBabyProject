using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGameManager : MonoBehaviour
{
    public static TextGameManager Instance;

    public List<Sprite> textSprites;
    public List<Sprite> alreadyDrawingTextSprites;
    public SpriteRenderer textRenderer;
    public SpriteRenderer alreadyDrawingTextRenderer;
    public int index;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        textRenderer = GetComponent<SpriteRenderer>();
        alreadyDrawingTextRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }
    public void Init(int textIndex = -1)
    {
        if (textIndex == -1)
        {
            textRenderer.sprite = textSprites[index];
            alreadyDrawingTextRenderer.sprite = alreadyDrawingTextSprites[index];
        }
        else
        {
            textRenderer.sprite = textSprites[textIndex];
            alreadyDrawingTextRenderer.sprite = alreadyDrawingTextSprites[textIndex];
        }
    }
}
