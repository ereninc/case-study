using DG.Tweening;
using UnityEngine;

public class MachineIconModel : TransformObject
{
    [SerializeField] private Transform parent;
    [SerializeField] private SpriteRenderer iconSprite;
    [SerializeField] private SpriteRenderer circleIcon;
    [SerializeField] private Color[] colors;

    private Tweener _animation;
    private bool _isCompleted = false;

    public void SetIcon(ProductDataSO productData)
    {
        iconSprite.sprite = productData.icon;
    }

    public void ToggleIconColor(bool isCompleted)
    {
        _isCompleted = isCompleted;
        circleIcon.color = _isCompleted ? colors[0] : colors[1];
        SetAnimation();
        parent.PunchScale();
    }

    private void SetAnimation()
    {
        if (_isCompleted)
        {
            _animation ??= parent.DOScale(Vector3.one * 1.1f, 0.6f).From(0.95f).SetEase(Ease.InSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            _animation?.Kill();
            _animation = null;
            parent.localScale = Vector3.one;
        }
    }
}