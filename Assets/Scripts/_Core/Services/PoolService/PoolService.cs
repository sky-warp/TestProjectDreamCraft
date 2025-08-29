using System.Collections.Generic;
using UnityEngine;

public abstract class PoolService<Pooltype, ElementType> where Pooltype : IPoolable
{
    private readonly IProvider<Pooltype, ElementType> _provider;

    private readonly Dictionary<ElementType, ObjectsPool> _pools = new();
    private readonly Transform _container;
    private readonly int _initialPoolSize;
    private readonly DIContainer _diContainer;
    
    protected PoolService(IProvider<Pooltype, ElementType> provider, Transform container,
        DIContainer diContainer, int initialSize)
    {
        _provider = provider;
        _container = container;
        _initialPoolSize = initialSize;
        _diContainer = diContainer;
    }

    public Pooltype GetElement(ElementType elementType) => 
        (Pooltype)_pools[elementType].GetPoolableObject();

    public void Initialize(ElementType elementType)
    {
        if (_pools.ContainsKey(elementType) == false)
        {
            _pools.Add(elementType, 
                new ObjectsPool(
                    new ObjectsPoolFactory(_provider.GetElement(elementType), _diContainer, 
                        _container), _initialPoolSize));
        }
    }
}