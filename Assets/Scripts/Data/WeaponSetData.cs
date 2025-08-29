using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "WeaponSet")]
public class WeaponSetData : ScriptableObject
{
    public WeaponData[] Weapons;
}