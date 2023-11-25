using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using Unity.VisualScripting;
using UnityEngine;

public class PaintCauldronModel : TransformObject
{
    [SerializeField] private TimerIcon timer;
    [SerializeField] private MeshRenderer liquidRenderer;
    [SerializeField] private int liquidMaterialIndex;

    public void OnInitialize(Color color)
    {
        liquidRenderer.materials[liquidMaterialIndex].color = color;
        timer.Initialize();
    }

    public void OnStartedPainting(float paintingDuration, Action onComplete)
    {
        timer.Transform.PunchScale();
        var paintingProcess = timer.StartTimer(paintingDuration);
        paintingProcess.OnComplete(() => onComplete?.Invoke());
    }

    public void OnFinishedPainting()
    {
        timer.Transform.PunchScale();
    }

    public void OnProductSold()
    {
        timer.SetActiveGameObject(false);
    }
}