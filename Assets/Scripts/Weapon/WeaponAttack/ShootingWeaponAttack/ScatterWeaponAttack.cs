using UnityEngine;

public class ScatterWeaponAttack : ShootingWeaponAttack
{
    [SerializeField]
    private float _spreadAngle;

    [Tooltip("Number of bullets per shot")]
    [SerializeField]
    private int _bulletCount;

    private void ShootBullets()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            Bullet bullet = GetBullet();
            bullet.SetPosition(BulletSpawnPoint.position);
            bullet.SetRotation(GetRandomBulletRotation());
            bullet.Activate();
        }
    }

    private Quaternion GetRandomBulletRotation()
    {
        float randomAngle = Random.Range(-_spreadAngle, _spreadAngle);
        return Quaternion.Euler(
            new Vector3(0, 0, BulletSpawnPoint.rotation.eulerAngles.z + randomAngle));
    }

    protected override void Shoot() => ShootBullets();
}