using System;

public class HeroWeaponServiceAdapter : IInitilizable,
    IDisposable
{
    private HeroWeaponService _heroWeaponService;
    private HeroWeaponView _heroWeaponView;

    [DInjection]
    public void Construct(HeroWeaponService heroWeaponService, HeroWeaponView heroWeaponView)
    {
        _heroWeaponService = heroWeaponService;
        _heroWeaponView = heroWeaponView;
    }

    private void SetWeaponSpriteView() => 
        _heroWeaponView.SetSprite(_heroWeaponService.NextWeapon.Sprite);

    public void Initialize()
    {
        SetWeaponSpriteView();
        
        _heroWeaponService.OnActivateWeapon += SetWeaponSpriteView;
    }

    public void Dispose()
    {
        _heroWeaponService.OnActivateWeapon -= SetWeaponSpriteView;
    }
}