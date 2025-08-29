using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private GameInstaller _gameInstaller;

    [SerializeField]
    private EntryPoint entryPoint;

    private void Awake()
    {
        entryPoint.Initialize();
        DIContainer diContainer = new DIContainer();
        InitializableService initializableService = new InitializableService();

        _gameInstaller.Initialize(diContainer, initializableService, entryPoint);
        _gameInstaller.InstallBinding();
        
        diContainer.InjectAll();
        initializableService.InitializeAll();
        
        entryPoint.StartGame();
    }
}
