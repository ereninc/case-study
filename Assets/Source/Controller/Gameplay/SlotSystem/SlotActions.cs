using System;
using JetBrains.Annotations;

public static class SlotActions
{
    #region [ Add / Remove ]

    public static Action<IDraggable?> OnAddDraggable;
    public static void Invoke_OnAddDraggable([CanBeNull] IDraggable draggable)
    {
        OnAddDraggable?.Invoke(draggable);
    }
    
    public static Action<IDraggable?> OnRemoveDraggable;
    public static void Invoke_OnRemoveDraggable([CanBeNull] IDraggable draggable)
    {
        OnRemoveDraggable?.Invoke(draggable);
    }

    #endregion
    
    #region [ Collect / Sold ]

    public static Action OnDraggableCollected;
    public static void Invoke_OnDraggableCollected()
    {
        OnDraggableCollected?.Invoke();
    }
    
    public static Action OnDraggableSold;
    public static void Invoke_OnDraggableSold()
    {
        OnDraggableSold?.Invoke();
    }

    #endregion
}