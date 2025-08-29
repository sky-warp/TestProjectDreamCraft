using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MeleeWeaponAttack : WeaponAttack
{
    [SerializeField]
    private float _damage;

    [SerializeField]
    private Animator _animator;

    private List<IDamageble> _targets = new();
    private bool _isActive;

    private void AddTarget(IDamageble target)
    {
        if (_targets.Contains(target) == false)
        {
            _targets.Add(target);
        }
    }
    
    private void RemoveTarget(IDamageble target)
    {
        if (_targets.Contains(target))
        {
            _targets.Remove(target);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isActive && other.gameObject.TryGetComponent(out IDamageble damageble))
        {
            RemoveTarget(damageble);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_isActive && col.gameObject.TryGetComponent(out IDamageble damageble))
        {
            AddTarget(damageble);
        }
    }

    private void Hit()
    {
        _animator.SetTrigger("Hit");

        for (int i = 0; i < _targets.Count; i++)
        {
            IDamageble target = _targets[i];
            if (_targets[i] != null)
            {
                target.TakeDamage(_damage);
            }
            else
            {
                RemoveTarget(target);
            }
        }
    }
    
    public override void Attack() => Hit();

    private void OnEnable() => _isActive = true;
    private void OnDisable() => _isActive = false;

}