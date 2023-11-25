using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ProductCount : TransformObject
{
    [SerializeField] private TextMeshProUGUI productCountText;
    private int _count = 0;

    private void Start()
    {
        Transform.localScale = Vector3.zero;
    }

    private void Added()
    {
        Transform.localScale = Vector3.one;
        _count++;
        productCountText.transform.PunchScale();
        productCountText.text = _count.ToString();
    }

    private void Removed()
    {
        _count--;
        productCountText.transform.PunchScale();
        productCountText.text = _count.ToString();
        if (_count <= 0) Transform.localScale = Vector3.one;
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        SlotActions.OnDraggableCollected += Added;
        SlotActions.OnDraggableSold += Removed;
    }

    private void OnDisable()
    {
        SlotActions.OnDraggableCollected -= Added;
        SlotActions.OnDraggableSold -= Removed;
    }

    #endregion
}