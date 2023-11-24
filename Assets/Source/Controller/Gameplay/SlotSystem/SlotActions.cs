using System;

public static class SlotActions
{
    public static Action<IDraggable> OnDraggableSpawned;
    public static void Invoke_OnDraggableSpawned(IDraggable draggable)
    {
        OnDraggableSpawned?.Invoke(draggable);
    }
    
    public static Action<IDraggable> OnDraggableUsed;
    public static void Invoke_OnDraggableUsed(IDraggable draggable)
    {
        OnDraggableUsed?.Invoke(draggable);
    }
}