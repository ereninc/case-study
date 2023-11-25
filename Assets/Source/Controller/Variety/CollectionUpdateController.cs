using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CollectionUpdateController : ControllerBaseModel
{
    private void OnSellProduct(int increaseAmount, Vector3 productPosition, int moneyAmount = 1)
    {
        UserPrefs.IncreaseCoinAmount(increaseAmount);
        EventController.Invoke_OnCoinUpdated();
        AudioController.PlaySound(AudioController.Sound.CollectionUpdate);
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        EventController.OnProductSell += OnSellProduct;
    }

    private void OnDisable()
    {
        EventController.OnProductSell -= OnSellProduct;
    }

    #endregion
}