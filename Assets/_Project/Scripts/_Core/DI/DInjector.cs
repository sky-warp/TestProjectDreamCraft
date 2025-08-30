using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DInjector
{
    private readonly BinderStorage _binderStorage;
    private readonly InjectorStorage _injectorStorage;
    
    private readonly Type OBJECT_TYPE = typeof(object);
    private readonly Type ATTRIBUTE_TYPE = typeof(DInjection);

    public DInjector(BinderStorage binderStorage, InjectorStorage injectorStorage)
    {
        _binderStorage = binderStorage;
        _injectorStorage = injectorStorage;
    }

    public void InjectAll()
    {
        object[] injects = _injectorStorage.GetObjects;
        for (int i = 0; i < injects.Length; i++)
        {
            if (injects[i] is MonoBehaviour mono)
            {
                InjectMono(mono);
            }
            else
            {
                Inject(injects[i]);
            }
        }
    }

    public void InjectMono(MonoBehaviour mono)
    {
        MonoBehaviour[] childs = mono.GetComponentsInChildren<MonoBehaviour>(true);
        for (int i = 0; i < childs.Length; i++)
        {
            _injectorStorage.Inject(childs[i]);
            Inject(childs[i]);
        }
    }

    private void Inject(object target)
    {
        var type = target.GetType();

        while (true)
        {
            if (type == null || type == OBJECT_TYPE)
            {
                break;
            }

            InjectByMethods(target, type);
            type = type.BaseType;
        }
    }

    private void InjectByMethods(object target, Type targetType)
    {
        var methods = targetType.GetMethods(BindingFlags.Instance |
                                            BindingFlags.Public |
                                            BindingFlags.DeclaredOnly);

        for (int i = 0, count = methods.Length; i < count; i++)
        {
            var method = methods[i];
            if (method.IsDefined(ATTRIBUTE_TYPE))
            {
                InjectByMethod(target, method);
            }
        }
    }
    
    private void InjectByMethod(object target, MethodInfo method)
    {
        var parameters = method.GetParameters();
        var count = parameters.Length;

        var args = new object[count];
        for (var i = 0; i < count; i++)
        {
            var parameter = parameters[i];
            var parameterType = parameter.ParameterType;

            if (_binderStorage.TryGetBindObject(parameterType, out var arg) == false)
            {
                LogWarning(parameterType);
            }

            args[i] = arg;
        }

        method.Invoke(target, args);
    }

    private void LogWarning(Type serviceType)
    {
#if UNITY_EDITOR
        Debug.LogWarning($"Can't inject missing service {serviceType.Name}!");
#endif
    }
}
