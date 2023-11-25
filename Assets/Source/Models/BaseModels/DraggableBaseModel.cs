using UnityEngine;

public class DraggableBaseModel : TransformObject, IDraggable
{
    private bool _isDragging = false;
    
    protected bool IsCompleted;
    public Transform productParent;
    
    public virtual void OnPointerDown()
    {
        _isDragging = false;
    }

    public virtual void OnPointerUpdate()
    {
        if (!_isDragging) return;
        // Transform.position = Input.mousePosition;
    }

    public virtual void OnPointerUp(DraggableSlot slot, float duration)
    {
        _isDragging = false;
    }

    public virtual void OnSelect()
    {
    }
    
    public virtual void OnDeselect()
    {
    }
    
    public virtual void OnPlaced(DraggableSlot slot, float duration){}
}