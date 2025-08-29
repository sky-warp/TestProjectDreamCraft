using System;
using UnityEngine;

public class MobileInput : IInput,
    IInitilizable
{
    public Vector2 Move => _input.Mobile.Move.ReadValue<Vector2>();
    
    public event Action<Vector2> OnShoot;
    public event Action OnChangeWeapon;
    
    private InputActions _input;
    
    public void Initialize()
    {
        _input = new InputActions();
        _input.Enable();

        _input.Mobile.ChangeWeapon.performed += _ => OnChangeWeapon?.Invoke();
        _input.Mobile.Shoot.started += _ => OnShoot?.Invoke(Input.GetTouch(0).position);
    }
}