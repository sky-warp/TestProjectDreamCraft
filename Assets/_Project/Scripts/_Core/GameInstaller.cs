using System;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField]
    private WeaponSetData _heroSetData;

    [SerializeField]
    private EnemySet _enemySet;

    [SerializeField]
    private HeroContainer _hero;

    [SerializeField]
    private GameBoard _gameBoard;

    [SerializeField]
    private HeroHealthView _heroHealthView;

    [SerializeField]
    private HeroWeaponView _heroWeaponView;

    [SerializeField]
    private MobileInputPanelView _mobileInputPanelView;
    
    [SerializeField]
    private KeyboardInputPanelView _keyboardInputPanelView;

    [SerializeField]
    private Transform _poolContainer;
    
    private DIContainer _diContainer;
    private InitializableService _initializableService;
    private EntryPoint _entryPoint;
    private AbstractFactory _abstractFactory;
    private DisposableService _disposableService;

    public void InstallBinding()
    {
        BindData();
        BindServices();
        BindPools();
        BindPrefabs();

        BindAndInject(new HeroWeaponService());
        BindAndInject(new EnemySpawner());
        BindAndInject(new HeroListener());
        BindAndInject(new InputAdapter());
        BindAndInject(new GameReloadService());

        InjectInstance(new HeroWeaponServiceAdapter());
        InjectInstance(new EnemyListener());
        InjectInstance(new HeroHealthViewAdapter());
        InjectInstance(_entryPoint);

        BindAndInjectPrefab(_abstractFactory.Create(_hero));
    }

    private void BindData()
    {
        _diContainer.Bind(_enemySet);
        _diContainer.Bind(_heroSetData);
    }

    private void BindServices()
    {
        BindInstance(new CoroutineService(this));
        BindInstance(_disposableService);
        BindInput();
    }

    private void BindInput()
    {
#if UNITY_EDITOR
        _abstractFactory.Create(_keyboardInputPanelView);
        BindInstance(new KeyboardInput());
#else
       _abstractFactory.Create(_mobileInputPanelView);
       BindInstance(new MobileInput());
#endif
    }

    private void BindPrefabs()
    {
        _diContainer.Bind(_abstractFactory.Create(_gameBoard));
        _diContainer.Bind(_abstractFactory.Create(_heroHealthView));
        _diContainer.Bind(_abstractFactory.Create(_heroWeaponView));
    }

    private void BindAndInjectPrefab(MonoBehaviour mono)
    {
        _diContainer.Bind(mono);
        _diContainer.Inject(mono);
    }

    private void BindPools()
    {
        AssetProvider assetProvider = new AssetProvider(new AssetPath());
        assetProvider.LoadAll();

        _diContainer.Bind(
            new BulletPoolService(assetProvider, _poolContainer, _diContainer, initialSize: 15));
        
        _diContainer.Bind(
            new EnemyPoolService(assetProvider, _poolContainer, _diContainer, initialSize: 6));
    }

    private void BindAndInject(object instance)
    {
        BindInstance(instance);
        InjectInstance(instance);
    }

    private void BindInstance(object instance)
    {
        _diContainer.Bind(instance);
        if(instance is IInitilizable initilizable)
            _initializableService.Add(initilizable);
        
        if(instance is IDisposable disposable)
            _disposableService.Add(disposable);
    }

    private void InjectInstance(object instance)
    {
        _diContainer.Inject(instance);
        if(instance is IInitilizable initilizable)
            _initializableService.Add(initilizable);
        
        if(instance is IDisposable disposable)
            _disposableService.Add(disposable);
    }

    public void Initialize(DIContainer diContainer, InitializableService initializableService,
        EntryPoint entryPoint)
    {
        _diContainer = diContainer;
        _initializableService = initializableService;
        _entryPoint = entryPoint;
        
        _abstractFactory = new AbstractFactory();
        _disposableService = new DisposableService();
    }
}