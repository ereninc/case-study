using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/LevelSewingMachineData")]
public class LevelSewingMachineData : ScriptableObject
{
    public List<SewingMachineData> sewingMachineData;
}