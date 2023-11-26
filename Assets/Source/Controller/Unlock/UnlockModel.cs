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
        ChangeState(LockState.Locked);
        Transform.PunchScale();
    }

    public void SetUnlockable()
    {
        ChangeState(LockState.Unlockable);
    }

    public void SetUnlocked()
    {
        ChangeState(LockState.Unlocked);
        Transform.PunchShrink();
        boxCollider.enabled = false;
        Transform.SetActiveGameObject(false);
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
        Debug.Log("Clicked");
        if (_unlockLevel - 1 <= UserPrefs.GetCurrentLevel())
        {
            var totalCollection = UserPrefs.GetTotalCollection();
            if (totalCollection < _unlockPrice)
            {
                SetLocked();
                Debug.Log("LOCKED!!");
            }
            else if (totalCollection >= _unlockPrice)
            {
                SetUnlocked();
                UserPrefs.DecreaseCoinAmount(_unlockPrice);
                EventController.Invoke_OnCoinUpdated();
                OnUnlocked?.Invoke();
                Debug.Log("UNLOCKED!!");
            }
        }
        else
        {
            SetLocked();
            Debug.Log("LOCKED ELSE!!");
        }
    }
}