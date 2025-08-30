using UnityEngine;

public class HeroWeaponDataFactory : AbstractFactory
{
    private WeaponData GetInstanceWeaponData(Weapon instance, Sprite sprite)
    {
        WeaponData newWeaponData = ScriptableObject.CreateInstance<WeaponData>();
        newWeaponData.Weapon = instance;
        newWeaponData.Sprite = sprite;
        return newWeaponData;
    }
    
    public WeaponData[] CreateWeaponInSet(WeaponSetData weaponSetData)
    {
        WeaponData[] prefabWeaponDatas = weaponSetData.Weapons;
        WeaponData[] instanceWeaponDatas = new WeaponData[prefabWeaponDatas.Length];

        for (int i = 0; i < prefabWeaponDatas.Length; i++)
        {
            Weapon prefab =  weaponSetData.Weapons[i].Weapon;
            Weapon instance = Create(prefab);

            instanceWeaponDatas[i] =
                GetInstanceWeaponData(instance, prefabWeaponDatas[i].Sprite);
        }

        return instanceWeaponDatas;
    }
}