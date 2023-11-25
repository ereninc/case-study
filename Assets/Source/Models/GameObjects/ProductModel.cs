using System;
using DG.Tweening;
using UnityEngine;

public class ProductModel : TransformObject
{
    [SerializeField] private SewingDataSO sewingData;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ProductDataSO productDataSO;

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
        sewingSequence.OnComplete(() => onComplete?.Invoke());
    }

    private void SetRenderer(float process)
    {
        meshRenderer.material.SetFloat("_CutoffHeight", process);
    }

    #region [ OnPaintArea Visual ]

    public void OnPaintAreaSlots()
    {
        Transform.localScale = Vector3.one * 1.5f;
        Transform.localRotation = Quaternion.Euler(45, -15, 0);
    }

    public void OnPlacedCauldron()
    {
        Transform.localScale = Vector3.one;
        Transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public Tweener OnStartedPainting(Color color)
    {
        return meshRenderer.material.DOColor(color, productDataSO.paintingTime);
    }

    #endregion
}