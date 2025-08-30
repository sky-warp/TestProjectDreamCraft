using System;
using UnityEngine;

public class Health : MonoBehaviour,
    IDamageble
{
    [SerializeField]
    private float _initValue;
    
    private float _value;
    
    public event Action OnDead;
    public event Action<float> OnChangedDelta;

    private float GetDelta() => _value / _initValue;
    
    public void TakeDamage(float damage)
    {
        if (_value - damage <= 0)
        {
            _value = 0;
            OnDead?.Invoke();
        }
        else
        {
            _value -= damage;
            OnChangedDelta?.Invoke(GetDelta());
        }
    }

    public void Reload()
    {
        _value = _initValue;
        OnChangedDelta?.Invoke(GetDelta());
    }
}