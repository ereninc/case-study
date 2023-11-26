using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaintingArea : ObjectModel
{
    [SerializeField] private ColorDataSO colors;

    public List<PaintCauldron> paintCauldrons;

    public void SetPaintCauldrons(LevelPaintCauldronData data)
    {
        for (int i = 0; i < paintCauldrons.Count; i++)
        {
            paintCauldrons[i].Initialize(data.paintCauldronData[i],
                GetColorDataByType(data.paintCauldronData[i].colorType));
        }
    }

    private ColorData GetColorDataByType(ColorType type)
    {
        return colors.colors.FirstOrDefault(color => color.type == type);
    }
}