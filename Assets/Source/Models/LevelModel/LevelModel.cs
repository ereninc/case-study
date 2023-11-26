using UnityEngine;

public class LevelModel : ObjectModel
{
    [SerializeField] private SlotController ropeSlotController;
    [SerializeField] private SlotController productSlotController;

    [SerializeField] private SewingArea sewingArea;
    [SerializeField] private PaintingArea paintingArea;

    public LevelDataSO LevelData { get; set; }

    public void Initialize(LevelDataSO levelData)
    {
        LevelData = levelData;
        SetAreaData();
    }

    private void SetAreaData()
    {
        sewingArea.SetMachines(LevelData.machineData);
        paintingArea.SetPaintCauldrons(LevelData.paintCauldronData);
    }
}