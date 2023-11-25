using System;
using DG.Tweening;
using UnityEngine;

public class Product : DraggableBaseModel
{
    [SerializeField] private DraggableSettingsDataSO draggableSettingsData;
    [SerializeField] private Vector3 offset;
    private ProductVisualModel _visualModel;

    public Action OnCompleted;

    public void OnInitialize(ProductVisualModel visualModel)
    {
        _visualModel = visualModel;
        _visualModel.SetVisual(productParent);
    }

    public void OnStartSewing(float sewingDuration)
    {
        IsCompleted = false;
        _visualModel.OnStartSewing(sewingDuration, () =>
        {
            SewingActions.OnProductCreated?.Invoke();
            OnCompleted?.Invoke();
            IsCompleted = true;
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
        if ((Product)product != this) return;
        Transform.TweenScaleShrink(1.2f, 0f, 0.3f, OnPaintArea);
    }

    private void OnPaintArea()
    {
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

    public override void OnPointerUp(DraggableSlot slot, float duration)
    {
        IsDragging = false;
        OnPlaced(slot, duration);
    }

    public override void OnSelect()
    {
        if (IsCompleted)
        {
            OnMovePaintArea();
            IsCompleted = false;
        }

        Transform.TweenScale(draggableSettingsData.selectedScaleMultiplier,
            draggableSettingsData.placeMovementDuration);
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

    //WHEN SELL IT AFTER PAINTING
    private void OnReturnPool()
    {
        Destroy(_visualModel.gameObject);
        Transform.ResetLocal();
        SetDeactive();
    }
}