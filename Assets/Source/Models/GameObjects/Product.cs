using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Product : TransformObject, IDraggable
{
    [SerializeField] private DraggableSettingsDataSO draggableSettingsData;
    [SerializeField] private Transform productParent;
    [SerializeField] private Vector3 offset;

    private ProductVisualModel _visualModel;
    private bool _isDragging = false;
    private bool _isCompleted;

    public Action OnCompleted;

    public void OnInitialize(ProductVisualModel visualModel)
    {
        _visualModel = visualModel;
        _visualModel.SetVisual(productParent);
    }

    public void OnStartSewing(float sewingDuration)
    {
        _isCompleted = false;
        _visualModel.OnStartSewing(sewingDuration, () =>
        {
            SewingActions.OnProductCreated?.Invoke();
            OnCompleted?.Invoke();
            _isCompleted = true;
        });
    }

    #region [ Paint Area ]

    private void OnMovePaintArea()
    {
        Vector3 worldPosition = TransitionExtension.UIToWorldPosition(AreaButtonController.Instance.GetRect(),
            CameraController.Instance.uiCamera, offset);
        var sequence = DOTween.Sequence();
        sequence.Append(Transform.DOLocalMove(new Vector3(0, -0.35f, -1), 0.15f));
        sequence.Join(Transform.DOLocalRotate(Vector3.zero, 0.15f));
        sequence.Append(Transform.DOScale(1.2f, 0.15f).From(1f));
        sequence.Append(Transform.DOScale(1f, 0.15f));
        sequence.OnComplete(() =>
        {
            Transform.DOMove(worldPosition, 0.35f).OnComplete(() => SewingActions.Invoke_OnProductReached(this));
        });
    }

    private void OnReachedPaintButton(IDraggable product)
    {
        if (product != this) return;
        var sequence = DOTween.Sequence();
        sequence.Append(Transform.DOScale(1.2f, 0.15f).From(1f));
        sequence.Append(Transform.DOScale(0f, 0.15f));
        sequence.OnComplete(OnPaintArea); //CHANGE HERE WITH OnPaintArea
    }

    private void OnPaintArea()
    {
        //SEND IT TO PAINT SLOTS
        PaintingActions.Invoke_OnEnterPaintingArea(this);
        _visualModel.OnPaintArea();
    }

    #endregion

    #region [ Subscriptions ]

    private void OnEnable()
    {
        SewingActions.OnProductReached += OnReachedPaintButton;
    }

    private void OnDisable()
    {
        SewingActions.OnProductReached -= OnReachedPaintButton;
    }

    #endregion

    #region [ IDraggable ]

    public void OnPointerDown()
    {
        _isDragging = false;
        OnSelect();
    }

    public void OnPointerUpdate()
    {
        if (!_isDragging) return;
        Transform.position = Input.mousePosition;
    }

    public void OnPointerUp(DraggableSlot slot, float duration)
    {
        _isDragging = false;
        OnPlaced(slot, duration);
    }

    public void OnSelect()
    {
        if (_isCompleted)
        {
            OnMovePaintArea();
            _isCompleted = false;
        }

        Transform.TweenScale(draggableSettingsData.selectedScaleMultiplier,
            draggableSettingsData.placeMovementDuration);
    }

    public void OnDeselect()
    {
        Transform.TweenScale();
    }

    private void OnPlaced(DraggableSlot targetSlot, float duration)
    {
        Transform.SetParent(targetSlot.Transform);
        // visualModel.OnPlaced();
        Transform.ResetLocalTween(draggableSettingsData.placedScaleFactor, draggableSettingsData.placeMovementDuration)
            .OnComplete(() =>
            {
                targetSlot.OnItemPlaced?.Invoke();
                OnStartSewing(duration);
            });
    }

    #endregion

    private void OnReturnPool()
    {
        Destroy(_visualModel.gameObject);
        Transform.ResetLocal();
        SetDeactive();
    }
}