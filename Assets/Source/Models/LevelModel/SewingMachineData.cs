using UnityEngine;

[CreateAssetMenu(menuName = "SO/SewingMachineData")]
public class SewingMachineData : ScriptableObject
{
    public ProductTypes type;
    public Sprite icon;
    public int unlockLevel;
    public int unlockPrice;
}