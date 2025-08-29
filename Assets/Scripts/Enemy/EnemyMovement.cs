using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Transform _rotatePoint;
    
    private Transform _target;
    private bool _isActive;

    [DInjection]
    public void Construct(HeroContainer target)
    {
        _target = target.transform;
    }

    private void FixedUpdate()
    {
        TurnToTarget();
        MoveToTarget();
    }

    private void TurnToTarget()
    {
        Vector2 direction = _target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        _rotatePoint.rotation = q;
    }

    private void MoveToTarget()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        _rb.MovePosition(_rb.position + direction * _speed * Time.fixedDeltaTime);
    }
}