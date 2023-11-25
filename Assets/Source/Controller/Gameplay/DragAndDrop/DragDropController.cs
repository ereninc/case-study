using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class DragDropController : Singleton<DragDropController>
{
    [SerializeField] private PointerController pointerController;
    [ShowInInspector] private IDraggable _draggableObject;
    [ShowInInspector] private IDroppable _droppableObject;
    private RaycastHit _hit;
    private Ray _ray;

    private void Update()
    {
        if (GameController.currentGameState != GameStates.Game) return;
        pointerController.ControllerUpdate();
    }

    #region [ UnityEvent Functions ]

    public void OnPointerDown()
    {
        _droppableObject = null;
        OnRaycast();
    }

    public void OnPointer()
    {
        OnRaycast();
    }

    public void OnPointerUp()
    {
        _droppableObject = null;
    }

    #endregion

    //Fill fields if ray object returns any IDraggable/IDroppable objects.
    private void OnRaycast()
    {
        _ray = CameraController.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit))
        {
            if (_draggableObject != null)
            {
                if (_hit.transform.TryGetComponent(out _droppableObject))
                {
                    OnSlotSelected(_droppableObject);
                }
            }

            _draggableObject?.OnDeselect();
            if (_hit.transform.TryGetComponent(out _draggableObject)) OnObjectSelected();
        }
    }

    private void OnObjectSelected()
    {
        _draggableObject.OnPointerDown();
    }

    private void OnSlotSelected(IDroppable selectableSlot)
    {
        if (_draggableObject == null) return;
        selectableSlot.OnDrop(_draggableObject);
        _draggableObject = null;
    }

    private void OnReturnObjectToPool(IDraggable draggable)
    {
        _draggableObject = null;
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        ProductActions.OnSellProduct += OnReturnObjectToPool;
    }

    private void OnDisable()
    {
        ProductActions.OnSellProduct -= OnReturnObjectToPool;
    }

    #endregion
}