using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private Weapon[] _weapons;

    [SerializeField]
    private float _delayActivate;
    
    private const float _delayTryAttack = 0.2f;

    private IEnumerator _weaponsAttackWork;

    private IEnumerator WeaponsAttack()
    {
        WaitForSeconds delayTryAttack = new WaitForSeconds(_delayTryAttack);
        
        yield return new WaitForSeconds(_delayActivate);
        
        while (true)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                _weapons[i].TryAttack();
            }

            yield return delayTryAttack;
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].Activate();
        }

        _weaponsAttackWork = WeaponsAttack();
        StartCoroutine(_weaponsAttackWork);
    }

    private void OnDisable()
    {
        if (_weaponsAttackWork != null)
        {
            StopCoroutine(_weaponsAttackWork);
        }

        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].Deactivate();
        }
    }
}