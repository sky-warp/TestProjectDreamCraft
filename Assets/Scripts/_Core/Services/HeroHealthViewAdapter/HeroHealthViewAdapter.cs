using System;

public class HeroHealthViewAdapter : IInitilizable,
    IDisposable
{
    private Health _hero;
    private HeroHealthView _heroHealthView;

    [DInjection]
    public void Construct(HeroContainer heroContainer, HeroHealthView heroHealthView)
    {
        _hero = heroContainer.Health;
        _heroHealthView = heroHealthView;
    }

    private void EventOnChangeHealth(float value) => 
        _heroHealthView.UpdateHealthBar(value);

    public void Initialize()
    {
        _hero.OnChangedDelta += EventOnChangeHealth;
    }

    public void Dispose()
    {
        _hero.OnChangedDelta -= EventOnChangeHealth;
    }
}