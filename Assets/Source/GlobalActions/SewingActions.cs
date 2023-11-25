using System;
using JetBrains.Annotations;

public static class SewingActions
{
    #region [ OnRopePlaced ]

    public static Action<Rope> OnRopePlaced;
    public static void Invoke_OnRopePlaced(Rope rope)
    {
        OnRopePlaced?.Invoke(rope);
    }

    #endregion

    #region [ OnProduct Created ]

    public static Action<Product> OnProductCreated;
    public static void Invoke_OnProductCreated(Product product)
    {
        OnProductCreated?.Invoke(product);
    }

    #endregion

    #region [ OnProduct Moved To PaintArea ]

    public static Action<IDraggable> OnProductReached;
    public static void Invoke_OnProductReached(Product reachedProduct)
    {
        OnProductReached?.Invoke(reachedProduct);
    }

    #endregion
}