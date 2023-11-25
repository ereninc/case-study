using System;
using UnityEngine;

public static class EventController
{
    #region [ COLLECTIONS ]

    public static Action OnCoinUpdated;
    public static void Invoke_OnCoinUpdated()
    {
        OnCoinUpdated?.Invoke();
    }

    #endregion

    #region [ On Product Sell ]

    public static Action<int, Vector3, int> OnProductSell;
    public static void Invoke_OnProductSell(int productPrice, Vector3 position, int moneyAmount)
    {
        OnProductSell?.Invoke(productPrice, position, moneyAmount);
    }

    #endregion
}