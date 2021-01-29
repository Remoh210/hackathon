using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BeatAnimator : BeatComponent
{
    public Sprite[] sprites;
    public bool loop = true;
    public bool destroyOnEnd = false;

    private int index = 0;
    private Image image;


    void Awake()
    {
        image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnBeat()
    {
        if (!loop && index == sprites.Length) return;
        image.sprite = sprites[index];
        index++;
        if (index >= sprites.Length)
        {
            if (loop) index = 0;
            if (destroyOnEnd) Destroy(gameObject);
        }
    }

}
