using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SewingMachine : ObjectModel, IDroppable
{
    [SerializeField] private SewingMachineVisual visualModel;
    [SerializeField] private DraggableSlot draggableObjectParent;
    [SerializeField] private Transform productObjectPoint;

    private ProductTypes _currentType;
    private ProductDataSO _productData;
    private Product _product;

    //CHANGE TO INITIALIZE FUNCTION FOR LEVEL DESIGN AND REMOVE TEST DATA
    [Header("TEST DATA")] public ProductDataSO testData;

    private void Start()
    {
        Initialize(testData);
    }

    [Button]
    public void Initialize(ProductDataSO productData)
    {
        _productData = productData;
        visualModel.SetVisual(_productData);
    }

    public void OnDrop(IDraggable draggableObject)
    {
        if (!draggableObjectParent.IsEmpty) return;
        draggableObject.OnPointerUp(draggableObjectParent, _productData.sewingTime);
        draggableObjectParent.ToggleSlot();
    }

    private void OnStartSewing()
    {
        _product = ProductFactory.Instance.SpawnObject<Product>(_productData.type);
        _product.Transform.SetLocalPositionAndRotation(productObjectPoint.position, productObjectPoint.localRotation);
        _product.OnStartSewing(_productData.sewingTime);
        _product.OnCompleted += OnCompleteSewing;
    }

    private void OnCompleteSewing()
    {
        draggableObjectParent.ToggleSlot();
        visualModel.ToggleIcon(true);
        _product.OnCompleted -= OnCompleteSewing;
    }

    private void OnAvailableAgain(Product product)
    {
        if (_product != product) return;
        _product = null;
        visualModel.ToggleIcon(false);
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        draggableObjectParent.OnItemPlaced += OnStartSewing;
        PaintingActions.OnEnterPaintingArea += OnAvailableAgain;
    }

    private void OnDisable()
    {
        draggableObjectParent.OnItemPlaced -= OnStartSewing;
        PaintingActions.OnEnterPaintingArea -= OnAvailableAgain;
    }

    #endregion
}