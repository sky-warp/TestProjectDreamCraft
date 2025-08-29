using UnityEngine;

public class EnemyPoolService : PoolService<Enemy, EnemyType>
{
    public EnemyPoolService(IProvider<Enemy, EnemyType> provider, Transform container, 
        DIContainer diContainer, int initialSize)
        : base(provider, container, diContainer, initialSize)
    {
    }
}