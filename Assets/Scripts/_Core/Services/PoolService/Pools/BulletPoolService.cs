using UnityEngine;

public class BulletPoolService : PoolService<Bullet, BulletType>
{
    public BulletPoolService(IProvider<Bullet, BulletType> provider, 
        Transform container, DIContainer diContainer, int initialSize) 
        : base(provider, container, diContainer, initialSize)
    {
    }
}