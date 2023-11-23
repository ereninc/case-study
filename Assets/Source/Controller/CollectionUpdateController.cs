using Sirenix.OdinInspector;
using UnityEngine;

public class CollectionUpdateController : ControllerBaseModel
{
    [Button]
    public void Increase(int amount, Vector3 position)
    {
        UserPrefs.IncreaseCoinAmount(amount);
    }

    [Button]
    public void Decrease(int amount, Vector3 position)
    {
        UserPrefs.DecreaseCoinAmount(amount);
    }
}