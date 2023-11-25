using DG.Tweening;
using UnityEngine;

public class Rope : TransformObject, IDraggable
{
    [SerializeField] private DraggableSettingsDataSO draggableSettingsData;
    [SerializeField] private RopeVisualModel visualModel;
    [SerializeField] private Transform modelParent;
    private bool _isDragging = false;

    #region [ IDraggable ]

    public void OnPointerDown()
    {
        _isDragging = true;
        OnSelect();
    }

    public void OnPointerUpdate()
    {
        if (!_isDragging) return;
        // Transform.position = Input.mousePosition;
    }

    public void OnPointerUp(DraggableSlot slot, float duration = 0f)
    {
        _isDragging = false;
        OnPlaced(slot, duration);
    }

    #endregion

    public void OnInitialize()
    {
        visualModel.OnInitialize();
        SetVisual();
    }

    private void OnPlaced(DraggableSlot targetSlot, float duration)
    {
        // Transform.SetParent(targetSlot.Transform);
        visualModel.OnPlaced();
        var sequence = DOTween.Sequence();
        sequence.Append(Transform.DOJump(targetSlot.Transform.position, 0.75f, 1,
            draggableSettingsData.placeMovementDuration));
        sequence.Join(Transform.DOScale(Vector3.one * draggableSettingsData.placedScaleFactor, 0.35f));
        sequence.AppendCallback(() =>
        {
            SlotActions.Invoke_OnDraggableUsed(this);
            targetSlot.OnItemPlaced?.Invoke();
            OnStartSewing(duration);
        });
    }

    private void OnStartSewing(float duration)
    {
        //Return to pool OnComplete
        visualModel.StartWorking(duration, SetDeactive);
    }

    private void SetVisual()
    {
        modelParent.localScale = Vector3.one * draggableSettingsData.deselectedScale;
    }
    
    public void OnSelect()
    {
        modelParent.TweenScale(draggableSettingsData.selectedScaleMultiplier,
            draggableSettingsData.placeMovementDuration);
        visualModel.ToggleIndicatorColor(true);
    }

    public void OnDeselect()
    {
        modelParent.TweenScale(2f);
        visualModel.ToggleIndicatorColor(false);
    }
}