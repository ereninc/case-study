using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeFactory : Singleton<RopeFactory>
{
    public T SpawnObject<T>()
    {
        Rope rope = PoolFactory.Instance.GetDeactiveItem<Rope>(PoolEnum.Rope);
        rope.OnInitialize();
        rope.SetActiveGameObject(true);
        return (T)((object)rope);
    }
}