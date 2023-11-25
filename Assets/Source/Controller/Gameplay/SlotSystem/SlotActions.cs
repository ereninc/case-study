using System;
using JetBrains.Annotations;

public static class SlotActions
{
    #region [ With Type ]

    public static Action<IDraggable?> OnDraggableSpawned;
    public static void Invoke_OnDraggableSpawned([CanBeNull] IDraggable draggable)
    {
        OnDraggableSpawned?.Invoke(draggable);
    }
    
    public static Action<IDraggable?> OnDraggableUsed;
    public static void Invoke_OnDraggableUsed([CanBeNull] IDraggable draggable)
    {
        OnDraggableUsed?.Invoke(draggable);
    }

    #endregion
    
    #region [ Without Type ]

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