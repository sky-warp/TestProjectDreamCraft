using System;

public class HeroWeaponService : IInitilizable
{
    private WeaponSetData _weaponSetData;
    private WeaponData[] _weapons;
    private HeroAttack _target;
    private int _indexNextWeapon = 0;

    public WeaponData NextWeapon => _weapons[_indexNextWeapon];

    public event Action OnActivateWeapon;

    [DInjection]
    public void Construct(WeaponSetData weaponSetData, HeroContainer heroContainer)
    {
        _weaponSetData = weaponSetData;
        _target = heroContainer.HeroAttack;

        _weapons = new HeroWeaponDataFactory().CreateWeaponInSet(_weaponSetData);
        SetWeaponPrefabsInHero();
    }

    private void SetWeaponPrefabsInHero()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            _target.SetWeapon(_weapons[i].Weapon);
        }
    }

    private void ActivateInitWeapon() => ActivateWeapon(_weapons[0].Weapon);

    private void ActivateWeapon(Weapon weapon)
    {
        if (_target.CurrentWeapon != null)
        {
            _target.CurrentWeapon.Deactivate();
        }

        weapon.Activate();
        _target.SetActiveWeapon(weapon);
        OnActivateWeapon?.Invoke();
    }

    public void SetNextWeapon()
    {
        if (_indexNextWeapon == _weapons.Length - 1)
            _indexNextWeapon = 0;
        else
            _indexNextWeapon++;
    }

    public void ActivateNextWeapon()
    {
        ActivateWeapon(NextWeapon.Weapon);
    }

    public void Initialize() => ActivateInitWeapon();
}