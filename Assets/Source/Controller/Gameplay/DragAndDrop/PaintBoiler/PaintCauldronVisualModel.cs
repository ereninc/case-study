using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCauldronVisualModel : TransformObject
{
    [SerializeField] private TimerIcon timer;
    [SerializeField] private MeshRenderer liquidRenderer;
    [SerializeField] private int liquidMaterialIndex;
    

    public void OnInitialize(Color color)
    {
        liquidRenderer.materials[liquidMaterialIndex].color = color;
    }
}