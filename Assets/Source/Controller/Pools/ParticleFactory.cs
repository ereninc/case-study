using Sirenix.OdinInspector;
using UnityEngine;

public class ParticleFactory : Singleton<ParticleFactory>
{
    [SerializeField] private Vector3 sewingOffset;
    
    [Button]
    public void SpawnParticle(PoolEnum pool, Vector3 pos, Quaternion rot)
    {
        SpawnObject<ParticleModel>(pool, pos, rot);
    }

    public T SpawnObject<T>(PoolEnum poolEnum, Vector3 position, Quaternion rotation)
    {
        ParticleModel particle = PoolFactory.Instance.GetDeactiveItem<ParticleModel>(poolEnum);
        particle.SetPositionAndRotation(position, rotation);
        return (T)((object)particle);
    }

    private void OnProductCreated(Product product)
    {
        SpawnParticle(PoolEnum.SmokeParticle, product.Transform.position + sewingOffset, Quaternion.identity);
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        SewingActions.OnProductCreated += OnProductCreated;
    }

    private void OnDisable()
    {
        SewingActions.OnProductCreated -= OnProductCreated;
    }

    #endregion
}