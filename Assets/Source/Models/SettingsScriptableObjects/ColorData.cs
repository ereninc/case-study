using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorData
{
    public ColorType type;
    public Color color;
}

public enum ColorType
{
    Color1,
    Color2,
    Color3,
    Color4
}
