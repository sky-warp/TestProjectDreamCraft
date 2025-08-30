using System.Collections.Generic;

public class InitializableService
{
    private List<IInitilizable> _initilizables = new();

    public void Add(IInitilizable initilizable)
    {
        if(_initilizables.Contains(initilizable) == false)
            _initilizables.Add(initilizable);
    }

    public void InitializeAll()
    {
        for (int i = 0; i < _initilizables.Count; i++)
        {
            _initilizables[i].Initialize();
        }
    }
}