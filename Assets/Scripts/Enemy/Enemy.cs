using System;
using UnityEngine;

public class Enemy : MonoBehaviour,
    IActivable,
    IPoolable,
    IInitilizable
{
    [field: SerializeField]
    public EnemyType EnemyType;

    [SerializeField]
    private Health _health;

    public bool IsActive { get; private set; }
    public MonoBehaviour GetItem => this;

    public event Action<Enemy> OnDead;

    private void EventOnDead() => OnDead?.Invoke(this);

    public void SetPosition(Vector2 position) => transform.position = position;
    
    public void Activate()
    {
        gameObject.SetActive(true);
        IsActive = true;
        _health.Reload();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        IsActive = false;
    }

    public void Initialize()
    {
        _health.OnDead += EventOnDead;
    }

    private void OnDestroy()
    {
        _health.OnDead -= EventOnDead;
    }
}