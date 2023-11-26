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

    public static Action<Product, DraggableSlot> OnPaintingStarted;
    public static void Invoke_OnPaintingStarted(Product product, DraggableSlot slot)
    {
        OnPaintingStarted?.Invoke(product, slot);
    }

    public static Action<ColorData, Product> OnEnteredCauldron;
    public static void Invoke_OnEnteredCauldron(ColorData data, Product product)
    {
        OnEnteredCauldron?.Invoke(data, product);
    }

    #endregion

    #region [ On Painting Finished ]

    public static Action<Product> OnPaintingFinished;

    public static void Invoke_OnPaintingFinished(Product product)
    {
        OnPaintingFinished?.Invoke(product);
    }

    #endregion

    #region [ On Cauldron Slot Clicked ]

    public static Action<IDroppable> OnCauldronClicked;
    public static void Invoke_OnCauldronClicked(IDroppable droppable)
    {
        OnCauldronClicked?.Invoke(droppable);
    }

    #endregion
}