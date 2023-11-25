using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TargetProducts")]
public class TargetProductDataSO : ScriptableObject
{
    public List<TargetProductData> targetProducts;

    public Color GetColor(ColorDataSO container, ColorType type)
    {
        return (Color)container.colors.FirstOrDefault(color => color.type == type)?.color;
    }

    public Sprite GetSprite(ProductContainerSO container, ProductTypes type)
    {
        return container.productContainer.FirstOrDefault(sprite => sprite.type == type)?.icon;
    }
}