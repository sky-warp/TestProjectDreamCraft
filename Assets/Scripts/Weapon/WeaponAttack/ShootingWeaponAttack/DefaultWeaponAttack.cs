using UnityEngine;

public class DefaultWeaponAttack : ShootingWeaponAttack
{
    private Quaternion GetBulletRotation() => 
        Quaternion.Euler(BulletSpawnPoint.rotation.eulerAngles);

    protected override void Shoot()
    {
        Bullet bullet = GetBullet();
        bullet.SetPosition(BulletSpawnPoint.position);
        bullet.SetRotation(GetBulletRotation());
        bullet.Activate();
    }
}