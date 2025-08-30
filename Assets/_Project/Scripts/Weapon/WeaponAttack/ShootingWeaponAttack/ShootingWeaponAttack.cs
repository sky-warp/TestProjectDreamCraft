using UnityEngine;

public abstract class ShootingWeaponAttack : WeaponAttack
{
    [SerializeField]
    private BulletType _bulletType;
    
    [SerializeField]
    private Transform _bulletSpawnPoint;
    
    private BulletPoolService _bulletPoolService;
    
    protected Transform BulletSpawnPoint => _bulletSpawnPoint; 
    
    [DInjection]
    public void Construct(BulletPoolService bulletPoolService)
    {
        _bulletPoolService = bulletPoolService;
        _bulletPoolService.Initialize(_bulletType);
    }

    protected Bullet GetBullet() => _bulletPoolService.GetElement(_bulletType);

    protected abstract void Shoot();

    public override void Attack() => Shoot();
}