using System;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProductVisualModel : TransformObject
{
    [SerializeField] private SewingDataSO sewingData;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material paintableMaterial;
    [SerializeField] private ProductDataSO productDataSO;
    private MaterialPropertyBlock _propertyBlock;

    public void SetVisual(Transform product)
    {
        Transform.SetParent(product);
        Transform.SetPositionAndRotation(Vector3.zero, product.transform.localRotation);
        SetRenderer(sewingData.startAmount);
    }

    public void OnStartSewing(Action onComplete)
    {
        float process = sewingData.startAmount;
        SetRenderer(process);

        var sewingSequence = DOTween.Sequence();
        sewingSequence.Append(DOTween.To(() => process, x => process = x, sewingData.endAmount,
            productDataSO.sewingTime));
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
        Transform.localScale = Vector3.one * 1.5f;
        Transform.localRotation = Quaternion.Euler(45, -15, 0);
    }

    private void SetPaintableMaterial()
    {
        Material paintable = new Material(paintableMaterial);
        meshRenderer.material = paintable;
    }

    public void OnPlaced()
    {
        Transform.localScale = Vector3.one;
        Transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnStartPainting(Color color)
    {
        // Debug.Log(meshRenderer.material.name);
        // MaterialPropertyBlock block = new MaterialPropertyBlock();
        meshRenderer.material.DOColor(color, productDataSO.paintingTime);
    }

    #endregion
}