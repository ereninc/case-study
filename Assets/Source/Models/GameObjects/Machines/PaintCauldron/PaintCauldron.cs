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


    [Header("TEST DATA")] [SerializeField] private ColorDataSO colorDataSO; // GET THIS FROM SPAWNER
    [SerializeField] private ColorType _colorType;

    //REMOVE LATER
    private void Start()
    {
        Initialize(_colorType);
    }

    public void Initialize(ColorType colorType)
    {
        _colorData = GetColorDataByType(colorType);
        paintCauldronModel.OnInitialize(_colorData.color);
        stateController.SetIdle();
    }

    //MOVE TO CAULDRON SETTER
    private ColorData GetColorDataByType(ColorType type)
    {
        return colorDataSO.colors.FirstOrDefault(color => color.type == type);
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