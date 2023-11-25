using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public static class TransformExtensions
{
    public static Sequence PunchScale(this Transform target)
    {
        var seq = DOTween.Sequence();
        seq.Append(target.DOScale(1.2f, 0.15f).From(1f));
        seq.Append(target.DOScale(1f, 0.15f));
        return seq;
    }
    
    public static Sequence PunchShrink(this Transform target)
    {
        var seq = DOTween.Sequence();
        seq.Append(target.DOScale(1.2f, 0.25f).From(1f));
        seq.Append(target.DOScale(0f, 0.25f));
        return seq;
    }

    public static Sequence MoveToButton(this Transform target, Vector3 position, Action onComplete)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(target.DOLocalMove(new Vector3(0, -0.35f, -1), 0.15f));
        sequence.Join(target.DOLocalRotate(Vector3.zero, 0.15f));
        sequence.Append(target.DOScale(1.2f, 0.15f).From(1f));
        sequence.Append(target.DOScale(1f, 0.15f));
        sequence.OnComplete(() =>
        {
            target.DOMove(position, 0.75f).OnComplete(()=>onComplete?.Invoke());
        });
        return sequence;
    }
}