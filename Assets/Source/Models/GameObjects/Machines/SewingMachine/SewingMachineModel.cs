using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewingMachineModel : ObjectModel
{
    [SerializeField] private MachineIconModel machineIconModel;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform effectParent;
    
    public void SetVisual(SewingMachineData sewingMachineData)
    {
        machineIconModel.SetIcon(sewingMachineData);
    }

    public void ToggleIcon(bool isCompleted)
    {
        machineIconModel.ToggleIconColor(isCompleted);
        effectParent.SetActiveGameObject(isCompleted);
    }

    public void SetAnimation(bool isCompleted)
    {
        animator.Play(isCompleted ? "OnIdle" : "OnSewing", 0, 0);
    }
}