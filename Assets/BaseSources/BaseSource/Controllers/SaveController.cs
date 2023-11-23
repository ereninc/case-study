using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

public class SaveController : Singleton<SaveController>
{
    public CoinData CoinData;

    private void Awake()
    {
        //GET FUNCTIONS
        LoadCoinData();
    }

    #region [ COIN DATA ]
    
    private void SaveCoinData()
    {
        DataSave.SaveJson(PrefType.CoinData, CoinData);
    }

    private void LoadCoinData()
    {
        CoinData = DataSave.GetJson<CoinData>(PrefType.CoinData);
    }
    
    [Button]
    public void SetCoinAmount(int coinAmount)
    {
        CoinData.CoinAmount = coinAmount;
        SaveCoinData();
    }
    
    public int GetCoinAmount()
    {
        return CoinData.CoinAmount;
    }

    #endregion
}

[System.Serializable]
public class CoinData
{
    public int CoinAmount;
}