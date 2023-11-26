using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    [Header("Sewing Machine Data List - Contains Unlock Level - Price etc.")]
    public LevelSewingMachineData machineData;
    
    [Header("Paint Cauldron Data List - Contains Unlock Level - Price etc.")]
    public LevelPaintCauldronData paintCauldronData;
    
    [Header("Target Product for every Level")]
    public TargetProductDataSO targetProductData;
}