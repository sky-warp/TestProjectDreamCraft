using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "NewWeapon")]
public class WeaponData : ScriptableObject
{
    public Sprite Sprite;
    public Weapon Weapon;
}