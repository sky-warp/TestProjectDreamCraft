using System;
using UnityEngine;

public interface IInput
{
    public Vector2 Move { get; }
    public event Action<Vector2> OnShoot;
    public event Action OnChangeWeapon;
}