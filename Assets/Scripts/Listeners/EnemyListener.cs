using System;
using System.Collections.Generic;

public class EnemyListener : IInitilizable,
    IDisposable
{
    private readonly List<Enemy> _activeEnemies = new();
    private EnemySpawner _enemySpawner;
    
    [DInjection]
    public void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    private void AddEnemy(Enemy enemy)
    {
        _activeEnemies.Add(enemy);
        enemy.OnDead += DeactivateEnemy;
    }

    private void DeactivateEnemy(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
        enemy.OnDead -= DeactivateEnemy;
        enemy.Deactivate();
    }
    
    public void Initialize()
    {
        _enemySpawner.OnSpawn += AddEnemy;
    }

    public void Dispose()
    {
        _enemySpawner.OnSpawn -= AddEnemy;
        for (int i = 0; i < _activeEnemies.Count; i++)
        {
            DeactivateEnemy(_activeEnemies[i]);
        }
    }
}