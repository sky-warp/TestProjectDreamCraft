using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : IInitilizable
{
    private EnemySpawnData[] _enemySpawnDatas;
    private EnemyPoolService _enemyPool;
    private CoroutineService _coroutineService;
    private GameBoard _gameBoard;
    
    public event Action<Enemy> OnSpawn;

    [DInjection]
    public void Construct(EnemySet enemySet, EnemyPoolService enemyPoolService,
        CoroutineService coroutineService, GameBoard gameBoard)
    {
        _enemySpawnDatas = enemySet.EnemySpawnDatas;
        _enemyPool = enemyPoolService;
        _coroutineService = coroutineService;
        _gameBoard = gameBoard;
    }

    private void InitPools()
    {
        for (int i = 0; i < _enemySpawnDatas.Length; i++)
        {
            _enemyPool.Initialize(_enemySpawnDatas[i].Enemy);
        }
    }

    private IEnumerator WaitSpawn(EnemySpawnData enemySpawnData)
    {
        WaitForSeconds delay = new WaitForSeconds(enemySpawnData.Delay);
        
        yield return new WaitForSeconds(enemySpawnData.Wait);
        
        while (true)
        {
            Spawn(enemySpawnData.Enemy);
            yield return delay;
        }
    }

    private void Spawn(EnemyType enemyType)
    {
        Enemy enemy = _enemyPool.GetElement(enemyType);
        enemy.SetPosition(_gameBoard.GetRandomPointOutsideBoard());
        OnSpawn?.Invoke(enemy);
        enemy.Activate();
    }

    public void StartSpawn()
    {
        for (int i = 0; i < _enemySpawnDatas.Length; i++)
        {
            _coroutineService.StartCoroutine(WaitSpawn(_enemySpawnDatas[i]));
        }
    }

    public void Initialize()
    {
        InitPools();
    }
}