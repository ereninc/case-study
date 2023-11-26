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
    public ProductTypes Type => productDataSO.type;

    public void SetVisual(Transform product)
    {
        Transform.SetParent(product);
        Transform.SetPositionAndRotation(Vector3.zero, product.transform.localRotation);
        SetDissolveValue(sewingData.startAmount);
        SetIndicator();
    }

    public void OnStartSewing(Action onComplete)
    {
        outline.OnSelected();
        float process = sewingData.startAmount;
        SetDissolveValue(process);

        Sequence sewing = Extensions.DOSequenceWithCallback(process, sewingData.endAmount, productDataSO.sewingTime,
            value => SetDissolveValue(value));
        sewing.OnComplete(() => onComplete?.Invoke());
    }

    //Reverse Dissolve for Sewing Effect
    private void SetDissolveValue(float process)
    {
        if (meshRenderer == null) return;
        meshRenderer.material.SetFloat("_CutoffHeight", process);
    }

    private void SetIndicator()
    {
        if (selectIndicator != null) selectIndicator.SetActiveGameObject(false);
        ToggleSelectIndicator(false);
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
        EventController.Invoke_OnProductSell(productDataSO.incomeAmount, Transform.position,
            productDataSO.sellIconCount);
    }

    public void ToggleSelectIndicator(bool isSelected)
    {
        if (selectIndicator == null) return;
        _isSelected = isSelected;
        selectIndicator.color = _isSelected ? colors[0] : colors[1];
        selectIndicator.transform.PunchScale();
    }

    #endregion
}