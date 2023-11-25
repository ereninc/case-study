using System;
using DG.Tweening;
using UnityEngine;

public class ProductModel : TransformObject
{
    [Header("Data")]
    [SerializeField] private SewingDataSO sewingData;
    [SerializeField] private ProductDataSO productDataSO;
    [Header("Visual")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private exOutline outline;
    [Header("Indicator / Color")]
    [SerializeField] private Color[] colors;
    [SerializeField] private SpriteRenderer selectIndicator;
    
    private bool _isSelected = false;
    public ProductTypes GetType => productDataSO.type;

    public void SetVisual(Transform product)
    {
        Transform.SetParent(product);
        Transform.SetPositionAndRotation(Vector3.zero, product.transform.localRotation);
        SetRenderer(sewingData.startAmount);
        
        selectIndicator.SetActiveGameObject(false);
        ToggleSelectIndicator(false);
    }
    
    public void OnStartSewing(Action onComplete)
    {
        outline.OnSelected();
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
        outline.OnHide();
        Transform.localScale = Vector3.one * 1.35f;
        Transform.localRotation = Quaternion.Euler(0, -15, 0);
        selectIndicator.SetActiveGameObject(true);
    }

    public void OnPlacedCauldron()
    {
        Transform.localScale = Vector3.one;
        Transform.localRotation = Quaternion.Euler(0, 0, 0);
        selectIndicator.SetActiveGameObject(false);
    }

    public Tweener OnStartedPainting(Color color)
    {
        return meshRenderer.material.DOColor(color, productDataSO.paintingTime).OnComplete(outline.OnSelected);
    }

    public void OnSellPainted()
    {
        EventController.Invoke_OnProductSell(productDataSO.incomeAmount, Transform.position, productDataSO.sellIconCount);
    }

    public void ToggleSelectIndicator(bool isSelected)
    {
        _isSelected = isSelected;
        selectIndicator.color = _isSelected ? colors[0] : colors[1];
        selectIndicator.transform.PunchScale();
    }

    #endregion
}