using System;
using System.Linq;
using UnityEngine;

public class AssetProvider : IBulletProvider,
    IEnemyProvider
{
    private readonly AssetPath _assetPath;
    private Enemy[] _enemies;
    private Bullet[] _bullets;

    public AssetProvider(AssetPath assetPath)
    {
        _assetPath = assetPath;
    }

    public Bullet GetElement(BulletType elementType)
    {
        Bullet bullet = _bullets.FirstOrDefault(x => x.BulletType == elementType);

        if (bullet == null)
            throw new NullReferenceException();
            
        return bullet;
    }

    public Enemy GetElement(EnemyType elementType)
    {
        Enemy enemy = _enemies.FirstOrDefault(x => x.EnemyType == elementType);

        if (enemy == null)
            throw new NullReferenceException();
            
        return enemy;
    }

    public void LoadAll()
    {
        _enemies = Resources.LoadAll<Enemy>(_assetPath.EnemiesPath);
        _bullets = Resources.LoadAll<Bullet>(_assetPath.BulletsPath);
    }
}