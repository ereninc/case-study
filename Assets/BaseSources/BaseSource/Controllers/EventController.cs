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

    #region [ On Product Sell / MoneyIncrease - UI Money Spawn ]

    public static Action<int, Vector3, int> OnProductSell;
    public static void Invoke_OnProductSell(int increaseAmount, Vector3 worldPosition, int moneyIconAmount)
    {
        OnProductSell?.Invoke(increaseAmount, worldPosition, moneyIconAmount);
    }
    
    #endregion
}