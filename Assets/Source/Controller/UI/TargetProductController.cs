using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProductController : ControllerBaseModel
{
    [SerializeField] private List<TargetSlot> slots;
    [SerializeField] private TargetProductDataSO targetProductData;
    [SerializeField] private ColorDataSO colorData;
    [SerializeField] private ProductContainerSO productContainer;
    
    public override void Initialize()
    {
        base.Initialize();
        SetTargetSlots();
    }

    private void SetTargetSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var currentData = targetProductData.targetProducts[i];
            slots[i].Initialize(targetProductData.GetSprite(productContainer, currentData.productType), targetProductData.GetColor(colorData, currentData.colorType));
        }
    }
}
