using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PaintCauldron : DroppableBaseModel
{
    [SerializeField] private PaintCauldronModel visualModel;
    [ShowInInspector] private ColorData _colorData;
    private Product _product;


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
        visualModel.OnInitialize(_colorData.color);
    }

    private ColorData GetColorDataByType(ColorType type)
    {
        return colorDataSO.colors.FirstOrDefault(color => color.type == type);
    }

    public override void OnDrop(IDraggable draggableObject)
    {
        PaintingActions.Invoke_OnEnteredCauldron(_colorData);
        base.OnDrop(draggableObject);
        OnStartPainting();
    }

    private void OnStartPainting()
    {
        visualModel.OnStartedPainting(2);
    }

    private void SetProduct(Product product)
    {
        _product = product;
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        PaintingActions.OnPaintingStarted += SetProduct;
    }

    private void OnDisable()
    {
        PaintingActions.OnPaintingStarted -= SetProduct;
    }

    #endregion
}