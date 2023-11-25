using System;
using UnityEngine;

public class DraggableBaseModel : TransformObject, IDraggable
{
    protected bool IsDragging = false;
    protected bool IsCompleted;
    public Transform productParent;
    
    public virtual void OnPointerDown()
    {
        IsDragging = false;
        OnSelect();
    }

    public virtual void OnPointerUpdate()
    {
        if (!IsDragging) return;
        Transform.position = Input.mousePosition;
    }

    public virtual void OnPointerUp(DraggableSlot slot, float duration)
    {
        IsDragging = false;
    }

    public virtual void OnSelect()
    {
        Debug.Log("OnSelect");
    }

    public virtual void OnDeselect()
    {
        Transform.TweenScale();
        Debug.Log("OnDeselect");
    }
}