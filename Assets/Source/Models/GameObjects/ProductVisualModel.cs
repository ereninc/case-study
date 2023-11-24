using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProductVisualModel : TransformObject
{
    [SerializeField] private SewingDataSO sewingData;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material paintableMaterial;
    
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
        sewingSequence.OnComplete(() =>
        {
            SetPaintableMaterial();
            onComplete?.Invoke();
        });
    }

    private void SetRenderer(float process)
    {
        meshRenderer.material.SetFloat("_CutoffHeight", process);
    }

    #region [ OnPaintArea Visual ]
    
    public void OnPaintArea()
    {
        Transform.localScale = Vector3.one * 1.75f;
        Transform.localRotation = Quaternion.Euler(45, -15, 0);
    }

    private void SetPaintableMaterial()
    {
        meshRenderer.material = paintableMaterial;
    }

    #endregion
}