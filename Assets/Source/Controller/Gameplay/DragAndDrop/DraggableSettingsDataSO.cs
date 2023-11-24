using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/DraggableSettings")]
public class DraggableSettingsDataSO : ScriptableObject
{
    [Header("Scale Settings")]
    public float placedScaleFactor;
    public float selectedScaleMultiplier;
    public float deselectedScale;
    
    [Header("Placing Settings")]
    public float placeMovementDuration;
}