using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class EntryPoint : MonoBehaviour,
    IInitilizable
{
    [SerializeField]
    private LoadingCurtain _loadingCurtain;

    [SerializeField]
    private float _waitStart;
    
    private EnemySpawner _enemySpawner;
    private InputAdapter _inputAdapter;

    [DInjection]
    public void Construct(EnemySpawner enemySpawner, InputAdapter inputAdapter)
    {
        _enemySpawner = enemySpawner;
        _inputAdapter = inputAdapter;
    }

    private IEnumerator WaitStartGame()
    {
        yield return new WaitForSeconds(_waitStart);
        _enemySpawner.StartSpawn();
        _inputAdapter.Activate();
    }
    
    public void StartGame()
    {
        _loadingCurtain.Deactivate();
        StartCoroutine(WaitStartGame());
    }

    public void Initialize() => _loadingCurtain.Activate();
}