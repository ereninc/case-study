using System;

public static class PaintingActions
{
    #region [ On Product Enter Painting Area ]

    public static Action<Product> OnEnterPaintingArea;
    public static void Invoke_OnEnterPaintingArea(Product product)
    {
        OnEnterPaintingArea?.Invoke(product);
    }

    #endregion

    #region [ On Product Enter Painting Cauldron ]

    public static Action<Product> OnPaintingStarted;
    public static void Invoke_OnPaintingStarted(Product product)
    {
        OnPaintingStarted?.Invoke(product);
    }

    public static Action<ColorData> OnEnteredCauldron;
    public static void Invoke_OnEnteredCauldron(ColorData data)
    {
        OnEnteredCauldron?.Invoke(data);
    }

    #endregion

    #region [ On Painting Finished ]

    public static Action<Product> OnPaintingFinished;

    public static void Invoke_OnPaintingFinished(Product product)
    {
        OnPaintingFinished?.Invoke(product);
    }

    #endregion
}