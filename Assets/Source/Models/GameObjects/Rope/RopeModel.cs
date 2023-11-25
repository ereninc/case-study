using System;
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
    
    private bool _isSelected = false;

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

    public void OnInitialize()
    {
        SetParts();
        ToggleIndicatorColor(false);
        selectIndicator.SetActiveGameObject(true);
        Transform.localScale = Vector3.one;
    }

    private void SetParts()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].transform.localScale = Vector3.one;
        }
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