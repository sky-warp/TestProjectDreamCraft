using UnityEngine;

public class ObjectsPoolFactory
{
    private readonly IPoolable _prefab;
    private readonly Transform _container;
    private readonly DIContainer _diContainer;

    public ObjectsPoolFactory(IPoolable prefab, DIContainer diContainer, Transform container)
    {
        _prefab = prefab;
        _diContainer = diContainer;
        _container = container;
    }
    
    public IPoolable Create()
    {
        MonoBehaviour instance = GameObject.Instantiate(_prefab.GetItem, _container);
        _diContainer.InjectForcibly(instance);
        return (IPoolable)instance;
    }
}