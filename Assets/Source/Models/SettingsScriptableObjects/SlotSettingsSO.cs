using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SlotSettings")]
public class SlotSettingsSO : ScriptableObject
{
    public int maxSlotCount;
    public Vector2Int respawnTime;
}
