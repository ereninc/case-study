using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimerIcon : TransformObject
{
    [SerializeField] private SpriteRenderer fillSprite;

    public override void Initialize()
    {
        base.Initialize();
        UpdateMaterial(360);
    }

    [Button]
    public void ReloadSprite(float duration)
    {
        var process = 360f;
        UpdateMaterial(process);
        var sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => process, x => process = x, 0, duration).SetEase(Ease.Linear).OnUpdate(() => UpdateMaterial(process)));
        sequence.AppendCallback(() => fillSprite.transform.PunchScale());
    }

    private void UpdateMaterial(float value)
    {
        fillSprite.material.SetFloat("_Arc2", value);
    }
}