﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class RopeModel : TransformObject
{
    [Header("Indicator / Color")] 
    [SerializeField] private SpriteRenderer selectIndicator;
    [SerializeField] private Color[] colors;

    [Header("Parts")] 
    [SerializeField] private Transform[] parts;
    [SerializeField] private RopeDataSO ropeData;

    private bool _isSelected = false;

    public void OnInitialize()
    {
        SetPartScale();
        ToggleIndicatorColor(false);
        selectIndicator.SetActiveGameObject(true);
        Transform.localScale = Vector3.one;
    }

    public void StartWorking(float time, Action onComplete)
    {
        StartCoroutine(ClosePartsOverTime(time, onComplete));
    }

    private IEnumerator ClosePartsOverTime(float duration, Action onComplete)
    {
        float timeToConsume = duration / parts.Length;
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].DOScale(0f, timeToConsume);
            yield return new WaitForSeconds(timeToConsume);
        }
        onComplete?.Invoke();
    }

    private void SetPartScale()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].transform.localScale = Vector3.one;
        }
    }

    [Button]
    public void SetColor()
    {
        //CHECK LATER
    }

    public void OnPlaced()
    {
        selectIndicator.SetActiveGameObject(false);
    }

    public void ToggleIndicatorColor(bool isSelected)
    {
        _isSelected = isSelected;
        selectIndicator.color = _isSelected ? colors[0] : colors[1];
        selectIndicator.transform.PunchScale();
    }
}