using UnityEngine;

public class DroppableBaseModel : ObjectModel, IDroppable
{
    public DraggableSlot draggableSlot;
    public Transform productObject;
    
    public virtual void OnDrop(IDraggable draggableObject)
    {
        if (!draggableSlot.IsEmpty) return;
        draggableObject.OnPointerUp(draggableSlot, 2f);
        draggableSlot.ToggleSlot();
    }
}