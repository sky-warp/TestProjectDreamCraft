using System;

public class HeroListener : IInitilizable,
    IDisposable
{
    private Health _hero;
    private GameReloadService _gameReloadService;

    [DInjection]
    public void Construct(HeroContainer hero, GameReloadService gameReloadService)
    {
        _hero = hero.Health;
        _gameReloadService = gameReloadService;
    }

    private void ReloadGame() => _gameReloadService.Reload();

    public void Initialize()
    {
        _hero.Reload();
        _hero.OnDead += ReloadGame;
    }

    public void Dispose()
    {
        _hero.OnDead -= ReloadGame;
    }
}