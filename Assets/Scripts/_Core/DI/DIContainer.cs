using UnityEngine;

public class DIContainer
{
    private readonly BinderStorage _binderStorage;
    private readonly InjectorStorage _injectorStorage;
    private readonly DInjector _injector;

    public DIContainer()
    {
        _binderStorage = new BinderStorage();
        _injectorStorage = new InjectorStorage();
        _injector = new DInjector(_binderStorage, _injectorStorage);
    }

    public void InjectForcibly(MonoBehaviour mono) => _injector.InjectMono(mono);

    public void InjectAll() => _injector.InjectAll();
    
    public void Inject(object target) => _injectorStorage.Inject(target);
    public void Bind(object target) => _binderStorage.Bind(target);
}