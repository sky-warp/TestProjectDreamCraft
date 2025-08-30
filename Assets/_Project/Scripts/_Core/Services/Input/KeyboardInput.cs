using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : IInput,
    IInitilizable
{
    public Vector2 Move => _input.Keyboard.Move.ReadValue<Vector2>();
    public event Action<Vector2> OnShoot;
    public event Action OnChangeWeapon;
    
    private InputActions _input;
    
    public void Initialize()
    {
        _input = new InputActions();
        _input.Enable();

        _input.Keyboard.ChangeWeapon.performed += _ => OnChangeWeapon?.Invoke();
        _input.Keyboard.Shoot.started += _ => OnShoot?.Invoke(Mouse.current.position.value);
    }
}