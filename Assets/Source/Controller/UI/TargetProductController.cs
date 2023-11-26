using System;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class TargetProductController : Singleton<TargetProductController>
{
    [SerializeField] private List<TargetSlot> slots;
    [SerializeField] private ColorDataSO colorData;
    [SerializeField] private ProductContainerSO productContainer;
    private int _successfulProductCount = 0;

    public override void Initialize()
    {
        base.Initialize();
        SetTargetSlots();
    }

    [Button]
    public void SetTargetSlots()
    {
        _successfulProductCount = 0;
        var currentList = LevelController.ActiveLevel.LevelData.targetProductData.targetProducts;
        for (int i = 0; i < currentList.Count; i++)
        {
            var currentData = currentList[i];
            slots[i].Initialize(
                LevelController.ActiveLevel.LevelData.targetProductData.GetSprite(productContainer,
                    currentData.productType),
                LevelController.ActiveLevel.LevelData.targetProductData.GetColor(colorData, currentData.colorType));
        }
    }

    private void CheckSoldProduct(Product product)
    {
        var data = new TargetProductData
        {
            colorType = product.Color.type,
            productType = product.Type
        };

        var productList = LevelController.ActiveLevel.LevelData.targetProductData.targetProducts;
        for (int i = 0; i < productList.Count; i++)
        {
            if (data.colorType == productList[i].colorType &&
                data.productType == productList[i].productType)
            {
                if (slots[i].IsReached) continue;
                CheckGameState();
                slots[i].OnSold();
                break;
            }
        }
    }

    private void CheckGameState()
    {
        _successfulProductCount++;
        if (_successfulProductCount >= LevelController.ActiveLevel.LevelData.targetProductData.targetProducts.Count)
        {
            GameController.SetGameState(GameStates.Win);
        }
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        ProductActions.OnSellProduct += CheckSoldProduct;
        EventController.OnLevelCompleted += SetTargetSlots;
    }

    private void OnDisable()
    {
        ProductActions.OnSellProduct -= CheckSoldProduct;
        EventController.OnLevelCompleted -= SetTargetSlots;
    }

    #endregion
}