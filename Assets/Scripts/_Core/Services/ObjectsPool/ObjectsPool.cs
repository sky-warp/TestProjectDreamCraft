using System.Collections.Generic;
using System.Linq;

public class ObjectsPool
{
    private readonly ObjectsPoolFactory _factory;
    private List<IPoolable> _poolables;

    public ObjectsPool(ObjectsPoolFactory factory, int initialSize)
    {
        _factory = factory;
        InitPool(initialSize);
    }

    private void InitPool(int initialSize)
    {
        _poolables = new List<IPoolable>();

        for (int i = 0; i < initialSize; i++)
        {
            GetInstantiatePoolableObject();
        }
    }

    private IPoolable GetInstantiatePoolableObject()
    {
        IPoolable poolable = _factory.Create();
        if(poolable is IActivable activable)
            activable.Deactivate();
        
        if(poolable is IInitilizable initilizable)
            initilizable.Initialize();
        
        _poolables.Add(poolable);
        return poolable;
    }

    public IPoolable GetPoolableObject()
    {
        List<IPoolable> deactivateWaterDrops =
            _poolables.Where(waterDrop => waterDrop.IsActive == false).ToList();
            
        if (deactivateWaterDrops.Count == 0)
        {
            return GetInstantiatePoolableObject();
        }

        return deactivateWaterDrops.First();
    }
}