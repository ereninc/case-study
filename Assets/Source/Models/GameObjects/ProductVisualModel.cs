using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProductVisualModel : TransformObject
{
    [SerializeField] private SewingDataSO sewingData;
    [SerializeField] private MeshRenderer meshRenderer;

    public void SetVisual(Transform product)
    {
        Transform.SetParent(product);
        Transform.SetPositionAndRotation(Vector3.zero, product.transform.localRotation);
        SetRenderer(sewingData.startAmount);
    }

    public void OnStartSewing(float duration, Action onComplete)
    {
        float process = sewingData.startAmount;
        SetRenderer(process);

        var sewingSequence = DOTween.Sequence();
        sewingSequence.Append(DOTween.To(() => process, x => process = x, sewingData.endAmount, duration));
        sewingSequence.OnUpdate(() => SetRenderer(process));
        sewingSequence.OnComplete(() => onComplete?.Invoke());
    }

    private void SetRenderer(float process)
    {
        meshRenderer.material.SetFloat("_CutoffHeight", process);
    }
}