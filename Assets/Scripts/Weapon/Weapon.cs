using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour,
    IActivable
{
    [SerializeField]
    private WeaponAttack _weaponAttack;

    [SerializeField]
    private float _reachedTime;

    private bool _isReached = true;

    private IEnumerator WaitReached()
    {
        _isReached = false;
        yield return new WaitForSeconds(_reachedTime);
        _isReached = true;
    }

    public void TryAttack()
    {
        if (_isReached)
        {
            _weaponAttack.Attack();
            if(gameObject.activeSelf)
                StartCoroutine(WaitReached());
        }
    }

    public void SetParent(Transform parent) => transform.parent = parent;

    public void SetPosition(Vector2 position) => transform.position = position;

    public void Activate()
    {
        gameObject.SetActive(true);
        _isReached = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}