using System;
using DG.Tweening;
using UnityEngine;

public class Product : DraggableBaseModel
{
    [SerializeField] private ProductStateController stateController;
    [SerializeField] private DraggableSettingsDataSO draggableSettingsData;
    [SerializeField] private Vector3 offset;
    private ProductModel _productModel;
    private ColorData _colorData;

    public Action OnCompleted;

    public void OnInitialize(ProductModel visualModel)
    {
        _productModel = visualModel;
        _productModel.SetVisual(productParent);
        stateController.SetProduction();
    }

    #region [ Sewing Area ]

    public void OnStartSewing()
    {
        IsCompleted = false;
        _productModel.OnStartSewing(() =>
        {
            OnCompleted?.Invoke();
            IsCompleted = true;
            stateController.SetSewed();
        });
    }

    #endregion

    #region [ Painting Area ]

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
        // Transform.TweenScaleShrink(1.2f, 0f, 0.3f, OnPaintArea);
        OnPaintArea();
    }

    private void OnPaintArea()
    {
        PaintingActions.Invoke_OnEnterPaintingArea(this);
        _productModel.OnPaintAreaSlots();
    }

    private void OnEnterCauldron()
    {
        PaintingActions.Invoke_OnPaintingStarted(this);
    }

    private void OnStartPainting(Product product)
    {
        if (product != this) return;
        _productModel.OnStartedPainting(_colorData.color).OnComplete(OnPaintingFinished);
    }

    private void SetColorData(ColorData data)
    {
        _colorData = data;
    }

    private void OnPaintingFinished()
    {
        PaintingActions.Invoke_OnPaintingFinished(this);
        stateController.SetPainted();
        IsCompleted = true;
    }

    #endregion

    //WHEN SELL IT AFTER PAINTING
    private void OnReturnPool()
    {
        Destroy(_productModel.gameObject);
        Transform.ResetLocal();
        SetDeactive();
    }

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
            if (stateController.GetState() == ProductState.Sewed)
            {
                OnMovePaintArea();
            }
            else if (stateController.GetState() == ProductState.Painted)
            {
                Debug.Log("SOLD!!");
                SetDeactive();
            }

            IsCompleted = false;
        }

        // switch (stateController.GetState())
        // {
        //     case ProductState.Sewed:
        //         OnMovePaintArea();
        //         break;
        //     case ProductState.Painted:
        //         Debug.Log("SOLD!!!!");
        //         SetDeactive();
        //         break;
        // }

        Transform.TweenScale(draggableSettingsData.selectedScaleMultiplier,
            draggableSettingsData.placeMovementDuration);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        Transform.TweenScale();
    }

    public override void OnPlaced(DraggableSlot targetSlot, float duration)
    {
        //SET TIMER AND PARTICLE - ANIMATION STUFF
        base.OnPlaced(targetSlot, duration);
        Transform.DOJump(targetSlot.Transform.position, 0.75f, 1, draggableSettingsData.placeMovementDuration)
            .OnComplete(() =>
            {
                if (IsCompleted) return;
                targetSlot.OnItemPlaced?.Invoke();
                // if (stateController.GetState() != ProductState.Sewed) return;
                _productModel.OnPlacedCauldron();
                OnEnterCauldron();
            });
    }

    #endregion

    #region [ Subscriptions ]

    private void OnEnable()
    {
        SewingActions.OnProductReached += OnReachedPaintButton;
        PaintingActions.OnPaintingStarted += OnStartPainting;
        PaintingActions.OnEnteredCauldron += SetColorData;
    }

    private void OnDisable()
    {
        SewingActions.OnProductReached -= OnReachedPaintButton;
        PaintingActions.OnPaintingStarted -= OnStartPainting;
        PaintingActions.OnEnteredCauldron -= SetColorData;
    }

    #endregion
}