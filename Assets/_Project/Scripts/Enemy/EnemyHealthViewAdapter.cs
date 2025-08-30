using UnityEngine;

public class EnemyHealthViewAdapter : MonoBehaviour
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private HealthView _healthView;

    private void OnEnable()
    {
        _health.OnChangedDelta += _healthView.UpdateHealthBar;
    }

    private void OnDisable()
    {
        _health.OnChangedDelta -= _healthView.UpdateHealthBar;
    }
}