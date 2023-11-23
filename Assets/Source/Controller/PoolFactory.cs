using System.Linq;
using Sirenix.OdinInspector;

public class PoolFactory : Singleton<PoolFactory>
{
    public Pools[] pools;

    public T GetDeactiveItem<T>(PoolEnum poolEnum)
    {
        var pool = SelectPool(poolEnum);
        var pooledObject = pool.GetDeactiveItem<T>();
        return pooledObject;
    }

    private PoolModel SelectPool(PoolEnum pool)
    {
        var selectedPool = pools.FirstOrDefault(x => x.poolEnum == pool);
        if (selectedPool != null)
        {
            return selectedPool.poolModel;
        }

        return null;
    }
}

[System.Serializable]
public class Pools
{
    public PoolEnum poolEnum;
    public PoolModel poolModel;
}

public enum PoolEnum
{
    Pool1,
}