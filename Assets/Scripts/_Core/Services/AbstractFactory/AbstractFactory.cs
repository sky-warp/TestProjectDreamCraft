using UnityEngine;

public class AbstractFactory
{
    public T Create<T>(T prefab) where T : MonoBehaviour
    {
        T instance = Object.Instantiate(prefab);
        if (instance is IActivable activable)
            activable.Deactivate();

        return instance;
    }
}