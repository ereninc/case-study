using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SewingMachine : DroppableBaseModel
{
    [SerializeField] private SewingMachineModel sewingMachineModel;
    [SerializeField] private BoxCollider boxCollider;
    
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
        sewingMachineModel.SetVisual(_productData);
    }

    private void OnStartSewing()
    {
        boxCollider.enabled = false;
        _product = ProductFactory.Instance.SpawnObject<Product>(_productData.type);
        _product.Transform.SetLocalPositionAndRotation(productObject.position, productObject.localRotation);
        _product.OnStartSewing();
        _product.OnCompleted += OnCompleteSewing;
    }

    private void OnCompleteSewing()
    {
        draggableSlot.ToggleSlot(true);
        sewingMachineModel.ToggleIcon(true);
        _product.OnCompleted -= OnCompleteSewing;
    }

    private void OnAvailableAgain(Product product)
    {
        if (_product != product) return;
        _product = null;
        boxCollider.enabled = true;
        sewingMachineModel.ToggleIcon(false);
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        draggableSlot.OnItemPlaced += OnStartSewing;
        PaintingActions.OnEnterPaintingArea += OnAvailableAgain;
    }

    private void OnDisable()
    {
        draggableSlot.OnItemPlaced -= OnStartSewing;
        PaintingActions.OnEnterPaintingArea -= OnAvailableAgain;
    }

    #endregion
}