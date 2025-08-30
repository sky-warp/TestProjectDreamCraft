using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [SerializeField]
    private Transform _gunPosition;
    
    private Weapon _currentWeapon;

    public Weapon CurrentWeapon => _currentWeapon;

    private void TurnToTarget(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }

    public void Attack(Vector2 target)
    {
        TurnToTarget(target);
        _currentWeapon.TryAttack();
    }

    public void SetActiveWeapon(Weapon weapon) => _currentWeapon = weapon;

    public void SetWeapon(Weapon weapon)
    {
        weapon.SetParent(_gunPosition);
        weapon.SetPosition(_gunPosition.position);
    }
}