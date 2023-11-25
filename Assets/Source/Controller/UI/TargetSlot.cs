using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class TargetSlot : TransformObject
{
    [SerializeField] private Image productImage;

    public void Initialize(Sprite productSprite, Color productColor)
    {
        Transform.SetActiveGameObject(true);
        productImage.sprite = productSprite;
        productImage.color = productColor;
    }

    [Button]
    public void OnSold()
    {
        var seq = DOTween.Sequence();
        seq.Append(Transform.DOScale(1.2f, 0.15f).From(1f));
        seq.Append(Transform.DOScale(0f, 0.15f));
        seq.OnComplete(SetDeactive);
    }
}