using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _speed;

    private bool _isMove;

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move() => 
        _rb.MovePosition(_rb.position + (Vector2)transform.up * _speed * Time.fixedDeltaTime);
}