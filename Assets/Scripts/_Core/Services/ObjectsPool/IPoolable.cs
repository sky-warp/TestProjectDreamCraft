using UnityEngine;

public interface IPoolable
{
    public bool IsActive { get; }
    public MonoBehaviour GetItem { get; }
}