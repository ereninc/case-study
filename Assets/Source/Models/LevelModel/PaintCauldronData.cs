using UnityEngine;

[CreateAssetMenu(menuName = "SO/PaintCauldronData")]
public class PaintCauldronData : ScriptableObject
{
    public ColorType colorType;
    public int unlockLevel;
    public int unlockPrice;
}