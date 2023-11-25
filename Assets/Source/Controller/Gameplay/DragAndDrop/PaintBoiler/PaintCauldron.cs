using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PaintCauldron : DroppableBaseModel
{
    [SerializeField] private PaintCauldronVisualModel visualModel;
    [ShowInInspector] private ColorData _colorData;


    [Header("TEST DATA")] 
    [SerializeField] private ColorDataSO colorDataSO; // GET THIS FROM SPAWNER
    [SerializeField] private ColorType _colorType;

    //REMOVE LATER
    private void Start()
    {
        Initialize(_colorType);
    }

    public void Initialize(ColorType colorType)
    {
        _colorData = GetColorDataByType(colorType);
        visualModel.OnInitialize(_colorData.color);
    }

    private ColorData GetColorDataByType(ColorType type)
    {
        return colorDataSO.colors.FirstOrDefault(color => color.type == type);
    }

    public override void OnDrop(IDraggable draggableObject)
    {
        base.OnDrop(draggableObject);
        PaintingActions.Invoke_OnEnterPaintingCauldron(null, _colorData);
    }
}