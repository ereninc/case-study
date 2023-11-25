using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TimerIcon : TransformObject
{
    [SerializeField] private SpriteRenderer fillSprite;
    private Tweener _animation;

    public override void Initialize()
    {
        base.Initialize();
        UpdateMaterial(360);
    }

    public Sequence StartTimer(float duration)
    {
        Transform.SetActiveGameObject(true);
        SetAnimation(false);
        var process = 360f;
        UpdateMaterial(process);
        var sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => process, x => process = x, 0, duration).SetEase(Ease.Linear).OnUpdate(() => UpdateMaterial(process)));
        sequence.AppendCallback(() => SetAnimation(true));
        return sequence;
    }

    private void UpdateMaterial(float value)
    {
        fillSprite.material.SetFloat("_Arc2", value);
    }

    private void SetAnimation(bool isCompleted)
    {
        if (!isCompleted)
        {
            _animation ??= Transform.DOScale(Vector3.one * 1.1f, 0.6f).From(0.95f).SetEase(Ease.InSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            _animation?.Kill();
            _animation = null;
            Transform.localScale = Vector3.one;
        }
    }
}