using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardView : MonoBehaviour
{
    private Image img;
    public int childIndex;

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    public void SetImg(Sprite cardSprite)
    {
        img.sprite = cardSprite;
    }
}
