using System;

public static class ProductActions
{
    #region [ OnSell Painted Products ]

    public static Action<Product> OnSellProduct;
    public static void Invoke_OnSellProduct(Product product)
    {
        OnSellProduct?.Invoke(product);
    }

    #endregion
}