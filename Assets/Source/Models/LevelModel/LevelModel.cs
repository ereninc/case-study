using UnityEngine;

public class LevelModel : ObjectModel
{
    [SerializeField] private SlotController ropeSlotController;
    [SerializeField] private SlotController productSlotController;

    [SerializeField] private SewingArea sewingArea;
    [SerializeField] private PaintingArea paintingArea;

    public LevelDataSO levelData;

    public override void Initialize()
    {
        base.Initialize();
        sewingArea.SetMachines(levelData.machineData);
        paintingArea.SetPaintCauldrons(levelData.paintCauldronData);
    }
}