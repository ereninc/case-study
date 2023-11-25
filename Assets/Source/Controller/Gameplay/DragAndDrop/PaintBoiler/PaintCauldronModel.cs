using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCauldronModel : TransformObject
{
    [SerializeField] private TimerIcon timer;
    [SerializeField] private MeshRenderer liquidRenderer;
    [SerializeField] private int liquidMaterialIndex;

    public void OnInitialize(Color color)
    {
        liquidRenderer.materials[liquidMaterialIndex].color = color;
    }

    public void OnStartedPainting(float paintingDuration)
    {
        timer.SetActiveGameObject(true);
        timer.StartTimer(paintingDuration);
    }

    public void OnPaintingEnded()
    {
        timer.SetActiveGameObject(false);
    }
}