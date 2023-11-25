using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ProductContainer")]
public class ProductContainerSO : ScriptableObject
{
    public List<ProductDataSO> productContainer;
}