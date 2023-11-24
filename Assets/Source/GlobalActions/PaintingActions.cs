using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaintingActions
{
    #region [ On Product Enter Painting Area ]

    public static Action<Product> OnEnterPaintingArea;
    public static void Invoke_OnEnterPaintingArea(Product product)
    {
        OnEnterPaintingArea?.Invoke(product);
    }

    #endregion
}
