using System.Collections.Generic;

public class InjectorStorage
{
    private readonly List<object> _injectObjects = new();

    public void Inject(object inject) => _injectObjects.Add(inject);

    public object[] GetObjects => _injectObjects.ToArray();
}