using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewingMachineModel : ObjectModel
{
    [SerializeField] private MachineIconModel machineIconModel;
    [SerializeField] private Animator animator;


    public void SetVisual(ProductDataSO productData)
    {
        machineIconModel.SetIcon(productData);
    }

    public void ToggleIcon(bool isCompleted)
    {
        machineIconModel.ToggleIconColor(isCompleted);
    }

    public void SetAnimation(bool isCompleted)
    {
        animator.Play(isCompleted ? "OnIdle" : "OnSewing", 0, 0);
    }
}