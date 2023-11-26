using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PaintCauldron : DroppableBaseModel, IUnlockable, IMachine
{
    [SerializeField] private MachineStateController stateController;
    [SerializeField] private PaintCauldronModel paintCauldronModel;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private UnlockModel unlockModel;

    private PaintCauldronData _paintCauldronData;
    private ColorData _colorData;
    private Product _product;
    private bool _isPainting = false;
    
    public void Initialize(PaintCauldronData paintCauldronData, ColorData colorData)
    {
        _paintCauldronData = paintCauldronData;
        _colorData = colorData;
        paintCauldronModel.OnInitialize(_colorData.color);
        stateController.SetIdle();
    }
    
    public override void OnDrop(IDraggable draggableObject)
    {
        base.OnDrop(draggableObject);
        OnStartProcess();
        PaintingActions.Invoke_OnEnteredCauldron(_colorData, (Product)draggableObject);
    }
    
    private void SetProduct(Product product, DraggableSlot slot)
    {
        if (draggableSlot != slot) return;
        _product = product;
    }

    #region [ IMachine ]

    public void OnStartProcess()
    {
        stateController.SetInProduction();
        if (_isPainting) return;
        boxCollider.enabled = false;
        paintCauldronModel.OnStartedPainting(2, OnFinishProcess);
        _isPainting = true;
    }

    public void OnFinishProcess()
    {
        paintCauldronModel.OnFinishedPainting();
    }

    public void OnAvailableAgain(Product product)
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
    
    #region [ IUnlockable ]
    
    public void CheckUnlockable(int currentLevel)
    {
        if (_paintCauldronData.unlockLevel <= 0 && unlockModel.CurrentState != LockState.Unlocked)
        {
            unlockModel.SetUnlocked();
            return;
        }
        if (currentLevel + 1 < _paintCauldronData.unlockLevel)
        {
            unlockModel.SetLocked();
        }
        else if (currentLevel + 1 >= _paintCauldronData.unlockLevel)
        {
            unlockModel.SetUnlockable();
        }

        unlockModel.SetTitle(_paintCauldronData.unlockLevel, _paintCauldronData.unlockPrice);
    }

    public void OnUnlocked()
    {
        boxCollider.enabled = true;
        Transform.PunchScale();
    }

    #endregion

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