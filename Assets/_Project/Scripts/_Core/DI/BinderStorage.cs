using System;
using System.Collections.Generic;

public class BinderStorage
{
    private readonly List<object> _bindObjects = new();

    public void Bind(object arg) => _bindObjects.Add(arg);

    public bool TryGetBindObject(Type targetType, out object result)
    {
        for (int i = 0, count = _bindObjects.Count; i < count; i++)
        {
            var currentObject = _bindObjects[i];
            var currentType = currentObject.GetType();

            if (targetType.IsAssignableFrom(currentType))
            {
                result = currentObject;
                return true;
            }
        }

        result = default;
        return false;
    }
}