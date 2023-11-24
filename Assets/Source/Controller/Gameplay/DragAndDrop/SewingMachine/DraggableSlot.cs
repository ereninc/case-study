using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableSlot : TransformObject
{
    public Action OnItemPlaced;
    public bool IsEmpty { get; private set; } = true;

    public void ToggleSlot()
    {
        IsEmpty = !IsEmpty;
    }
}