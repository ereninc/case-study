using System;
using DG.Tweening;
using UnityEngine;

public class Product : DraggableBaseModel
{
    [SerializeField] private ProductStateController stateController;
    [SerializeField] private DraggableSettingsDataSO draggableSettingsData;
    [SerializeField] private Vector3 offset;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform modelParent;
    
    private DraggableSlot _currentSlot;
    private ProductModel _productModel;
    private ColorData _colorData;

    public ColorData Color => _colorData;
    public ProductTypes Type => _productModel.GetType;
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
            SewingActions.Invoke_OnProductCreated(this);
        });
    }

    #endregion

    #region [ Painting Area ]

    private void OnMovePaintArea()
    {
        Vector3 worldPosition = TransitionExtension.UIToWorldPosition(AreaButtonController.Instance.GetRect(), CameraController.Instance.uiCamera, offset);
        Transform.MoveToPosition(worldPosition, () => SewingActions.Invoke_OnProductReached(this));
    }

    private void OnReachedPaintButton(IDraggable product)
    {
        if ((Product)product != this) return;
        OnPaintArea();
    }

    private void OnPaintArea()
    {
        PaintingActions.Invoke_OnEnterPaintingArea(this);
        modelParent.ResetLocal();
        _productModel.OnPaintAreaSlots();
    }

    private void OnEnterCauldron()
    {
        if (stateController.GetState() != ProductState.Sewed) return;
        PaintingActions.Invoke_OnPaintingStarted(this, _currentSlot);
        SlotActions.Invoke_OnDraggableUsed(this);
        boxCollider.enabled = false;
        animator.enabled = true;
    }

    private void SetColorData(ColorData data, Product product)
    {
        if (product != this) return;
        _colorData = data;
    }

    private void OnStartPainting(Product product, DraggableSlot slot)
    {
        if (product != this) return;
        stateController.SetProduction();
        animator.Play("OnPainting");
        _productModel.OnStartedPainting(_colorData.color).OnComplete(OnPaintingFinished);
    }

    private void OnPaintingFinished()
    {
        PaintingActions.Invoke_OnPaintingFinished(this);
        Transform.MoveUp(0.1025f, 0.25f);
        stateController.SetPainted();
        boxCollider.enabled = true;
        IsCompleted = true;
        animator.Play("OnIdle");
        animator.enabled = false;
        modelParent.ResetLocal();
    }

    #endregion

    #region [ Sell Functions ]

    private void OnSellPainted()
    {
        if (stateController.GetState() != ProductState.Painted) return;
        stateController.SetSold();
        ProductActions.Invoke_OnSellProduct(this);
        SellAnimation();
    }

    private void SellAnimation()
    {
        Transform.DOMove(new Vector3(6, -0.15f, -1f), 1f).OnComplete(() =>
        {
            _productModel.OnSellPainted();
            OnReturnPool();
        });
    }

    private void OnReturnPool()
    {
        Transform.PunchShrink().OnComplete(() =>
        {
            Destroy(_productModel.gameObject);
            DOTween.Kill(Transform);
            Transform.ResetLocal();
            SetDeactive();
        });
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
            if (stateController.GetState() == ProductState.Sewed)
            {
                OnMovePaintArea();
            }
            else if (stateController.GetState() == ProductState.Painted)
            {
                OnSellPainted();
            }

            IsCompleted = false;
        }

        _productModel.ToggleSelectIndicator(true);
        Transform.TweenScale(draggableSettingsData.selectedScaleMultiplier,
            draggableSettingsData.placeMovementDuration);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        _productModel.ToggleSelectIndicator(false);
        Transform.TweenScale();
    }

    public override void OnPlaced(DraggableSlot targetSlot, float duration)
    {
        base.OnPlaced(targetSlot, duration);
        _currentSlot = targetSlot;
        if (stateController.GetState() != ProductState.Painted)
        {
            Transform.DOJump(targetSlot.Transform.position, 0.75f, 1, draggableSettingsData.placeMovementDuration)
                .OnComplete(() =>
                {
                    if (IsCompleted) return;
                    targetSlot.OnItemPlaced?.Invoke();
                    _productModel.OnPlacedCauldron();
                    OnEnterCauldron();
                    AudioController.Instance.PlaySound(AudioController.Sound.Painting);
                });
        }
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