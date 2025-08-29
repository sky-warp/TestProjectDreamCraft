using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private float _speed;
    
    public void Move(Vector2 position) => 
        _rb.MovePosition(_rb.position + position * _speed * Time.fixedDeltaTime);
}