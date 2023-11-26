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
    [SerializeField] private ParticleSystem liquidWaveParticle;
    [SerializeField] private ParticleSystem bubbleSurfaceParticle;
    
    
    
    [SerializeField] private int liquidMaterialIndex;
    private Color _color;

    public void OnInitialize(Color color)
    {
        _color = color;
        liquidRenderer.materials[liquidMaterialIndex].color = _color;
        SetParticleColor();
        timer.Initialize();
    }

    public void OnStartedPainting(float paintingDuration, Action onComplete)
    {
        timer.Transform.PunchScale();
        var paintingProcess = timer.StartTimer(paintingDuration);
        paintingProcess.OnComplete(() => onComplete?.Invoke());
        
        liquidWaveParticle.Play();
    }

    public void OnFinishedPainting()
    {
        timer.Transform.PunchScale();
        liquidWaveParticle.Stop();
    }

    public void OnProductSold()
    {
        timer.SetActiveGameObject(false);
    }

    private void SetParticleColor()
    {
        var liquidMainSettings = liquidWaveParticle.main;
        liquidMainSettings.startColor = _color;
        
        var surfaceMainSettings = bubbleSurfaceParticle.main;
        surfaceMainSettings.startColor = _color;
    }
}