using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    #region [ On Product Enter Painting Cauldron ]

    public static Action<Product?, ColorData> OnEnterPaintingCauldron;
    public static void Invoke_OnEnterPaintingCauldron([CanBeNull] Product product, ColorData colorData)
    {
        OnEnterPaintingCauldron?.Invoke(product, colorData);
    }

    #endregion
}