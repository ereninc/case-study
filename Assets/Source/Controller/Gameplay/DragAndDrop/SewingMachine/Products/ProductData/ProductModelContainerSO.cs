using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ProductContainer")]
public class ProductModelContainerSO : ScriptableObject
{
    public List<Products> products;
}

[System.Serializable]
public class Products
{
    public ProductTypes type;
    public ProductModel prefab;
}
