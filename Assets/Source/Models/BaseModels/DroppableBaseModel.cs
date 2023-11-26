using System;
using UnityEngine;

public class DroppableBaseModel : TransformObject, IDroppable
{
    public DraggableSlot draggableSlot;
    public Transform productObject;
    public MachineType machineType;
    
    public virtual void OnDrop(IDraggable draggableObject)
    {
        if (!draggableSlot.IsEmpty) return;
        draggableObject.OnPointerUp(draggableSlot, 2f);
        draggableSlot.ToggleSlot(false);
        OnTriggerAction();
    }

    private void OnTriggerAction()
    {
        switch (machineType)
        {
            case MachineType.SewingMachine:
                // Debug.Log("Clicked Sewing Machine");
                break;
            case MachineType.PaintingMachine:
                PaintingActions.Invoke_OnCauldronClicked(this);
                // Debug.Log(transform.name);
                break;
        }        
    }
}

public enum MachineType
{
    SewingMachine,
    PaintingMachine
}