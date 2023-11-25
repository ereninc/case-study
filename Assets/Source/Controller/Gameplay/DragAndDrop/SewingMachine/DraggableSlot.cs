using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DraggableSlot : TransformObject
{
    public Action OnItemPlaced;
    [ShowInInspector] public bool IsEmpty { get; private set; } = true;

    public void ToggleSlot(bool isEmpty)
    {
        IsEmpty = isEmpty;
    }
}