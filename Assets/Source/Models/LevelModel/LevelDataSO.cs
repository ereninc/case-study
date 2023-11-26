using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public LevelSewingMachineData machineData;
    public LevelPaintCauldronData paintCauldronData;
    public TargetProductDataSO targetProductData;
}