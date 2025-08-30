using System;
using System.Collections;
using UnityEngine;

public class InputAdapter : IInitilizable,
    IActivable,
    IDisposable
{
    private IInput _input;
    private CoroutineService _coroutineService;
    private HeroMovement _heroMovement;
    private HeroAttack _heroAttack;
    private HeroWeaponService _heroWeaponService;
    private Camera _camera;
    private bool _isActive;

    private IEnumerator _readMoveValueWork;

    [DInjection]
    public void Construct(IInput input, HeroContainer heroContainer, 
        CoroutineService coroutineService, HeroWeaponService heroWeaponService)
    {
        _input = input;
        _heroMovement = heroContainer.HeroMovement;
        _heroAttack = heroContainer.HeroAttack;
        _coroutineService = coroutineService;
        _heroWeaponService = heroWeaponService;
    }

    private Vector2 ConvertToWorldPosition(Vector2 viewPosition) =>
        _camera.ScreenToWorldPoint(viewPosition);

    private void ChangeWeapon()
    {
        if (_isActive)
        {
            _heroWeaponService.SetNextWeapon();
            _heroWeaponService.ActivateNextWeapon();
        }
    }

    private void Shoot(Vector2 targetPosition)
    {
        if(_isActive)
            _heroAttack.Attack(ConvertToWorldPosition(targetPosition));
    }

    private IEnumerator ReadMoveValue()
    {
        while (true)
        {
            if (_isActive)
            {
                Vector2 targetPosition = _input.Move;
                if (targetPosition != Vector2.zero)
                {
                    _heroMovement.Move(targetPosition);
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
    
    public void Initialize()
    {
        _camera = Camera.main;
        
        _input.OnShoot += Shoot;
        _input.OnChangeWeapon += ChangeWeapon;
        
        _readMoveValueWork = ReadMoveValue();
        _coroutineService.StartCoroutine(_readMoveValueWork);
    }

    public void Activate() => _isActive = true;

    public void Deactivate() => _isActive = false;

    public void Dispose()
    {
        _input.OnShoot -= Shoot;
        _input.OnChangeWeapon -= ChangeWeapon;
        
        if(_readMoveValueWork != null)
            _coroutineService.StopCoroutine(_readMoveValueWork);
    }
}