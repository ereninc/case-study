using System;

public static class ProductActions
{
    #region [ OnSell Which Products ]

    public static Action<Product> OnSellProduct;
    public static void Invoke_OnSellProduct(Product product)
    {
        OnSellProduct?.Invoke(product);
    }

    #endregion

    #region [ OnSellPrice ]

    public static Action<int> OnSellPrice;
    public static void Invoke_OnSellPrice(int price)
    {
        OnSellPrice?.Invoke(price);
    }

    #endregion
}