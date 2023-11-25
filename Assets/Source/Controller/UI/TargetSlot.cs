using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSlot : TransformObject
{
    [SerializeField] private Image productImage;

    public void Initialize(Sprite productSprite, Color productColor)
    {
        productImage.sprite = productSprite;
        productImage.color = productColor;
    }
}