using System.Collections.Generic;
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
        var currentList = LevelController.ActiveLevel.levelData.targetProductData.targetProducts;
        for (int i = 0; i < currentList.Count; i++)
        {
            var currentData = currentList[i];
            slots[i].Initialize(LevelController.ActiveLevel.levelData.targetProductData.GetSprite(productContainer, currentData.productType),
                LevelController.ActiveLevel.levelData.targetProductData.GetColor(colorData, currentData.colorType));
        }
    }

    private void CheckSoldProduct(Product product)
    {
        var data = new TargetProductData
        {
            colorType = product.GetColor.type,
            productType = product.GetType
        };

        var productList = LevelController.ActiveLevel.levelData.targetProductData.targetProducts;
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
        if (_successfulProductCount >= LevelController.ActiveLevel.levelData.targetProductData.targetProducts.Count)
        {
            GameController.SetGameState(GameStates.Win);
        }
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        ProductActions.OnSellProduct += CheckSoldProduct;
    }

    private void OnDisable()
    {
        ProductActions.OnSellProduct -= CheckSoldProduct;
    }

    #endregion
}