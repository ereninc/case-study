using System.Collections;
using UnityEngine;

public static class UserPrefs
{

    #region [ Collection Save ]

    public static int totalEarned = 0;
    public static int GetTotalCollection()
    {
        return LocalPrefs.GetInt(PrefType.CollectionObject.ToString(), 100);
    }

    public static void IncreaseCoinAmount(int amount)
    {
        var totalAmount = GetTotalCollection() + amount;
        totalEarned += amount;
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

    public static int GetTotalEarned()
    {
        return totalEarned;
    }

    #endregion

    #region [ Level Save ]

    public static int currentLevel = 0;

    public static int GetCurrentLevel()
    {
        return LocalPrefs.GetInt(PrefType.LevelNo.ToString(), 0);
    }

    public static void SetLevel(int levelIndex)
    {
        LocalPrefs.SetInt(PrefType.LevelNo.ToString(), levelIndex);
        Save();
    }

    #endregion
    

    public static void Save()
    {
        LocalPrefs.Save(LocalPrefs.defaultFileName, true);
    }
}