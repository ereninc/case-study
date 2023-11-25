using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewingMachineModel : ObjectModel
{
    [SerializeField] private MachineIconModel machineIconModel;
    
    public void SetVisual(ProductDataSO productData)
    {
        machineIconModel.SetIcon(productData);
    }

    public void ToggleIcon(bool isCompleted)
    {
        machineIconModel.ToggleIconColor(isCompleted);
    }
}