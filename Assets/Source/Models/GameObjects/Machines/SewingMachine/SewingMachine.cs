using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SewingMachine : DroppableBaseModel
{
    [SerializeField] private SewingMachineModel sewingMachineModel;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private UnlockModel unlockModel;

    private ProductTypes _currentType;
    private SewingMachineData _sewingMachineData;
    private Product _product;

    public void Initialize(SewingMachineData sewingMachineData)
    {
        _sewingMachineData = sewingMachineData;
        sewingMachineModel.SetVisual(_sewingMachineData);
        unlockModel.Initialize(_sewingMachineData.unlockLevel, _sewingMachineData.unlockPrice);
    }

    public void CheckUnlockable(int currentLevel)
    {
        if (_sewingMachineData.unlockLevel <= 0)
        {
            unlockModel.SetUnlocked();
            return;
        }

        if (currentLevel + 1 < _sewingMachineData.unlockLevel)
        {
            boxCollider.enabled = false;
            unlockModel.SetLocked();
        }
        else if (currentLevel + 1 >= _sewingMachineData.unlockLevel)
        {
            boxCollider.enabled = false;
            unlockModel.SetUnlockable();
        }

        unlockModel.SetTitle(_sewingMachineData.unlockLevel, _sewingMachineData.unlockPrice);
    }

    private void OnUnlocked()
    {
        boxCollider.enabled = true;
    }

    private void OnStartSewing()
    {
        boxCollider.enabled = false;
        _product = ProductFactory.Instance.SpawnObject<Product>(_sewingMachineData.type);
        _product.Transform.SetLocalPositionAndRotation(productObject.position, productObject.localRotation);
        _product.OnStartSewing();
        _product.OnCompleted += OnCompleteSewing;
        sewingMachineModel.SetAnimation(false);
    }

    private void OnCompleteSewing()
    {
        draggableSlot.ToggleSlot(true);
        sewingMachineModel.ToggleIcon(true);
        sewingMachineModel.SetAnimation(true);
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
        unlockModel.OnUnlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        draggableSlot.OnItemPlaced -= OnStartSewing;
        PaintingActions.OnEnterPaintingArea -= OnAvailableAgain;
        unlockModel.OnUnlocked -= OnUnlocked;
    }

    #endregion
}