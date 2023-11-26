using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MasterLevelData")]
public class MasterLevelDataSO : ScriptableObject
{
    public List<LevelDataSO> levelData;
}