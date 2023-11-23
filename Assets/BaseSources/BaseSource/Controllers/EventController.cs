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
}