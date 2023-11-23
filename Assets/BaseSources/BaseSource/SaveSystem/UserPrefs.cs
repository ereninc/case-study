using System.Collections;
using UnityEngine;

public static class UserPrefs
{
    public static int GetTotalCollection()
    {
        return LocalPrefs.GetInt(PrefType.CollectionObject.ToString(), 100);
    }

    public static void IncreaseCoinAmount(int amount)
    {
        var totalAmount = GetTotalCollection() + amount;
        SetTotalCoin(totalAmount);
    }

    public static void DecreaseCoinAmount(int amount)
    {
        var totalAmount = GetTotalCollection();
        if (totalAmount >= amount)
        {
            totalAmount = GetTotalCollection() - amount;
        }
        SetTotalCoin(totalAmount);
    }

    private static void SetTotalCoin(int totalAmount)
    {
        LocalPrefs.SetInt(PrefType.CollectionObject.ToString(), totalAmount);
        Save();
    }

    public static void Save()
    {
        LocalPrefs.Save(LocalPrefs.defaultFileName, true);
    }
}