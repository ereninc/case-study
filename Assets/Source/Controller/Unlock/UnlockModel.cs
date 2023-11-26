using System;
using TMPro;
using UnityEngine;

public class UnlockModel : DraggableBaseModel
{
    [SerializeField] private TextMeshPro lockTitle;
    [SerializeField] private BoxCollider boxCollider;

    private LockState _lockState;
    private int _unlockLevel;
    private int _unlockPrice;

    public Action OnUnlocked;
    public LockState CurrentState => _lockState;

    public void Initialize(int unlockLevel, int unlockPrice)
    {
        _unlockLevel = unlockLevel;
        _unlockPrice = unlockPrice;
    }

    private void ChangeState(LockState state)
    {
        _lockState = state;
    }

    public void SetLocked()
    {
        if (_lockState == LockState.Unlocked) return;
        ChangeState(LockState.Locked);
        Transform.PunchScale();
    }

    public void SetUnlockable()
    {
        if (_lockState == LockState.Unlocked) return;
        ChangeState(LockState.Unlockable);
    }

    public void SetUnlocked()
    {
        ChangeState(LockState.Unlocked);
        Transform.PunchShrink();
        boxCollider.enabled = false;
        Transform.SetActiveGameObject(false);
        AudioController.Instance.PlaySound(AudioController.Sound.ButtonClick);
        EventController.Invoke_OnUnlockMachine(this);
        OnUnlocked?.Invoke();
    }
    
    public void SetTitle(int unlockLevel, int unlockPrice)
    {
        switch (_lockState)
        {
            case LockState.Locked:
                lockTitle.text = "Level " + unlockLevel;
                break;
            case LockState.Unlockable:
                lockTitle.text = unlockPrice.ToString();
                break;
            case LockState.Unlocked:
                break;
        }
    }

    public override void OnPointerDown()
    {
        base.OnPointerDown();
        OnClick();
    }

    private void OnClick()
    {
        if (_unlockLevel - 1 <= UserPrefs.GetCurrentLevel())
        {
            var totalCollection = UserPrefs.GetTotalCollection();
            if (totalCollection < _unlockPrice)
            {
                SetLocked();
            }
            else if (totalCollection >= _unlockPrice)
            {
                SetUnlocked();
                UserPrefs.DecreaseCoinAmount(_unlockPrice);
                EventController.Invoke_OnCoinUpdated();
                OnUnlocked?.Invoke();
            }
        }
        else
        {
            SetLocked();
        }
    }
}