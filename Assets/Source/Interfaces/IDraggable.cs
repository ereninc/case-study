using UnityEngine;

public interface IDraggable
{
    //POINT & CLICK & UPDATE
    void OnPointerDown();
    void OnPointerUpdate();
    void OnPointerUp(DraggableSlot slot, float duration);
    
    //SELECTABLES
    void OnSelect();
    void OnDeselect();
}