using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PaintCauldron : DroppableBaseModel
{
    [SerializeField] private MachineStateController stateController;
    [SerializeField] private PaintCauldronModel paintCauldronModel;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private UnlockModel unlockModel;

    private PaintCauldronData _paintCauldronData;
    [ShowInInspector] private ColorData _colorData;
    [ShowInInspector] private Product _product;
    [ShowInInspector] private bool _isPainting = false;
    
    public void Initialize(PaintCauldronData paintCauldronData, ColorData colorData)
    {
        _paintCauldronData = paintCauldronData;
        _colorData = colorData;
        paintCauldronModel.OnInitialize(_colorData.color);
        stateController.SetIdle();
    }
    
    public void CheckUnlockable(int currentLevel)
    {
        if (_paintCauldronData.unlockLevel <= 0)
        {
            unlockModel.SetUnlocked();
            return;
        }

        if (currentLevel + 1 < _paintCauldronData.unlockLevel)
        {
            boxCollider.enabled = false;
            unlockModel.SetLocked();
        }
        else if (currentLevel + 1 >= _paintCauldronData.unlockLevel)
        {
            boxCollider.enabled = false;
            unlockModel.SetUnlockable();
        }

        unlockModel.SetTitle(_paintCauldronData.unlockLevel, _paintCauldronData.unlockPrice);
    }

    private void OnUnlocked()
    {
        boxCollider.enabled = true;
        Transform.PunchScale();
    }
    
    public override void OnDrop(IDraggable draggableObject)
    {
        base.OnDrop(draggableObject);
        OnStartPainting();
        PaintingActions.Invoke_OnEnteredCauldron(_colorData, (Product)draggableObject);
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
        unlockModel.OnUnlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        PaintingActions.OnPaintingStarted -= SetProduct;
        ProductActions.OnSellProduct -= OnAvailableAgain;
        unlockModel.OnUnlocked -= OnUnlocked;
    }

    #endregion
}