using System;
using UnityEngine;

public static class EventController
{
    #region [ Collections ]

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

    #region [ Level ]
    
    public static Action OnLevelCompleted;
    public static void Invoke_OnLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    
    #endregion
}