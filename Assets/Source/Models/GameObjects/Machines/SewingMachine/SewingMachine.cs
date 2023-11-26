using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SewingMachine : DroppableBaseModel, IUnlockable, IMachine
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

    #region [ IMachine ]
    
    public void OnStartProcess()
    {
        boxCollider.enabled = false;
        _product = ProductFactory.Instance.SpawnObject<Product>(_sewingMachineData.type);
        _product.Transform.SetLocalPositionAndRotation(productObject.position, productObject.localRotation);
        _product.OnStartSewing();
        _product.OnCompleted += OnFinishProcess;
        sewingMachineModel.SetAnimation(false);
    }

    public void OnFinishProcess()
    {
        draggableSlot.ToggleSlot(true);
        sewingMachineModel.ToggleIcon(true);
        sewingMachineModel.SetAnimation(true);
        _product.OnCompleted -= OnFinishProcess;
    }

    public void OnAvailableAgain(Product product)
    {
        if (_product != product) return;
        _product = null;
        boxCollider.enabled = true;
        sewingMachineModel.ToggleIcon(false);
    }

    #endregion
    
    #region [ IUnlockable ]

    public void CheckUnlockable(int currentLevel)
    {
        if (_sewingMachineData.unlockLevel <= 0 && unlockModel.CurrentState != LockState.Unlocked)
        {
            unlockModel.SetUnlocked();
            return;
        }

        if (currentLevel + 1 < _sewingMachineData.unlockLevel)
        {
            unlockModel.SetLocked();
        }
        else if (currentLevel + 1 >= _sewingMachineData.unlockLevel)
        {
            unlockModel.SetUnlockable();
        }

        unlockModel.SetTitle(_sewingMachineData.unlockLevel, _sewingMachineData.unlockPrice);
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
        draggableSlot.OnItemPlaced += OnStartProcess;
        PaintingActions.OnEnterPaintingArea += OnAvailableAgain;
        unlockModel.OnUnlocked += OnUnlocked;
    }

    private void OnDisable()
    {
        draggableSlot.OnItemPlaced -= OnStartProcess;
        PaintingActions.OnEnterPaintingArea -= OnAvailableAgain;
        unlockModel.OnUnlocked -= OnUnlocked;
    }

    #endregion
}