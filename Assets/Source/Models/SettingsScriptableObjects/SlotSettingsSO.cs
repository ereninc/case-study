using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SlotSettings")]
public class SlotSettingsSO : ScriptableObject
{
    public int maxRopeSlotCount;
    public int maxProductSlotCount;
    public Vector2Int respawnTime;
}
