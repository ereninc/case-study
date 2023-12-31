using DG.Tweening;
using UnityEngine;

public class Rope : DraggableBaseModel
{
    [SerializeField] private DraggableSettingsDataSO draggableSettingsData;
    [SerializeField] private RopeModel ropeModel;
    [SerializeField] private Transform modelParent;

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
        modelParent.TweenScale(draggableSettingsData.selectedScaleMultiplier,
            draggableSettingsData.placeMovementDuration);
        ropeModel.ToggleIndicatorColor(true);
        base.OnSelect();
    }

    public override void OnDeselect()
    {
        modelParent.TweenScale(2f);
        ropeModel.ToggleIndicatorColor(false);
        base.OnDeselect();
    }

    public override void OnPlaced(DraggableSlot targetSlot, float duration)
    {
        ropeModel.OnPlaced();
        modelParent.localRotation = Quaternion.identity;
        var sequence = DOTween.Sequence();
        sequence.Append(Transform.DOJump(targetSlot.Transform.position, 0.75f, 1,
            draggableSettingsData.placeMovementDuration));
        sequence.Join(Transform.DOScale(Vector3.one * draggableSettingsData.placedScaleFactor, 0.35f));
        sequence.AppendCallback(() =>
        {
            SlotActions.Invoke_OnRemoveDraggable(this);
            targetSlot.OnItemPlaced?.Invoke();
            OnStartSewing(duration);
        });
        base.OnPlaced(targetSlot, duration);
    }
    #endregion

    public void OnInitialize()
    {
        ropeModel.OnInitialize();
        SetVisual();
    }

    private void OnStartSewing(float duration)
    {
        //Return to pool OnComplete
        ropeModel.StartWorking(duration, SetDeactive);
    }

    private void SetVisual()
    {
        Transform.PunchScale();
        modelParent.localScale = Vector3.one * draggableSettingsData.deselectedScale;
    }
}