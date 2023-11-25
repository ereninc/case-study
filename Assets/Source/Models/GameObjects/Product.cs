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

    #region [ Sewing Area ]
    
    public void OnStartSewing()
    {
        IsCompleted = false;
        _visualModel.OnStartSewing(() =>
        {
            SewingActions.OnProductCreated?.Invoke();
            OnCompleted?.Invoke();
            IsCompleted = true;
        });
    }

    #endregion
    

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

    private void OnStartPainting(Product product, ColorData colorData)
    {
        if (product != this) return;
        _visualModel.OnStartPainting(colorData.color);
    }

    #endregion

    #region [ Subscriptions ]

    private void OnEnable()
    {
        SewingActions.OnProductReached += OnReachedPaintButton;
        PaintingActions.OnEnterPaintingCauldron += OnStartPainting;
    }

    private void OnDisable()
    {
        SewingActions.OnProductReached -= OnReachedPaintButton;
        PaintingActions.OnEnterPaintingCauldron -= OnStartPainting;
    }

    #endregion

    #region [ IDraggable ]

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        OnSelect();
    }

    public override void OnPointerUp(DraggableSlot slot, float duration)
    {
        base.OnPointerUp(slot, duration);
        OnPlaced(slot, duration);
    }

    public override void OnSelect()
    {
        base.OnSelect();
        if (IsCompleted)
        {
            OnMovePaintArea();
            IsCompleted = false;
        }

        Transform.TweenScale(draggableSettingsData.selectedScaleMultiplier,
            draggableSettingsData.placeMovementDuration);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        Transform.TweenScale();
    }

    private void OnPlaced(DraggableSlot targetSlot, float duration)
    {
        Transform.SetParent(targetSlot.Transform);
        //SET TIMER AND PARTICLE - ANIMATION STUFF
        _visualModel.OnPlaced();
        Transform.DOLocalJump(Vector3.zero, 0.75f, 1, draggableSettingsData.placeMovementDuration).OnComplete(() =>
        {
            targetSlot.OnItemPlaced?.Invoke();
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