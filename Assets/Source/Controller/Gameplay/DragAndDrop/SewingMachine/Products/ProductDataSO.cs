using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ProductData", fileName = "NewProduct")]
public class ProductDataSO : ScriptableObject
{
    public ProductTypes type;
    public Sprite icon;
    public int unlockLevel;
    public float sewingTime;
    public float paintingTime;
    public int incomeAmount;
    public int sellIconCount;
}