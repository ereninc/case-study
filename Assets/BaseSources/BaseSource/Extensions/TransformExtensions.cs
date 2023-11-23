using DG.Tweening;
using UnityEngine;

public static class TransformExtensions
{
    public static void PunchScale(this Transform target)
    {
        var seq = DOTween.Sequence();
        seq.Append(target.DOScale(1.2f, 0.15f).From(1f));
        seq.Append(target.DOScale(1f, 0.15f));
    }
}