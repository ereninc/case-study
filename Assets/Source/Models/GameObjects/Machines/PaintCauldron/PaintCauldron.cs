using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PaintCauldron : DroppableBaseModel
{
    [SerializeField] private MachineStateController stateController;
    [SerializeField] private PaintCauldronModel paintCauldronModel;
    [SerializeField] private BoxCollider boxCollider;
    
    [ShowInInspector] private ColorData _colorData;
    [ShowInInspector] private Product _product;
    [ShowInInspector] private bool _isPainting = false;
    
    public void Initialize(ColorData colorData)
    {
        _colorData = colorData;
        paintCauldronModel.OnInitialize(_colorData.color);
        stateController.SetIdle();
    }

    public override void OnDrop(IDraggable draggableObject)
    {
        base.OnDrop(draggableObject);
        OnStartPainting();
        PaintingActions.Invoke_OnEnteredCauldron(_colorData);
    }

    #region [ On Process ]

    private void OnStartPainting()
    {
        stateController.SetInProduction();
        if (_isPainting) return;
        boxCollider.enabled = false;
        paintCauldronModel.OnStartedPainting(2, OnFinishedPainting);
        _isPainting = true;
    }

    private void OnFinishedPainting()
    {
        paintCauldronModel.OnFinishedPainting();
    }

    private void OnAvailableAgain(Product product)
    {
        if (_product != product || _product == null) return;
        _product = null;
        paintCauldronModel.OnProductSold();
        draggableSlot.ToggleSlot(true);
        boxCollider.enabled = true;
        _isPainting = false;
        stateController.SetIdle();
    }

    #endregion
    
    private void SetProduct(Product product, DraggableSlot slot)
    {
        if (draggableSlot != slot) return;
        _product = product;
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        PaintingActions.OnPaintingStarted += SetProduct;
        ProductActions.OnSellProduct += OnAvailableAgain;
    }

    private void OnDisable()
    {
        PaintingActions.OnPaintingStarted -= SetProduct;
        ProductActions.OnSellProduct -= OnAvailableAgain;
    }

    #endregion
}