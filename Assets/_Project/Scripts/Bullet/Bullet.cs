using UnityEngine;

public class Bullet : MonoBehaviour,
    IActivable,
    IPoolable
{
    [field: SerializeField]
    public BulletType BulletType;

    [SerializeField]
    private float _damage;

    public MonoBehaviour GetItem => this;
    public bool IsActive { get; private set; }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out IDamageble damageble))
        {
            damageble.TakeDamage(_damage);
            Deactivate();
        }
    }

    public void SetPosition(Vector2 spawnPosition) => transform.position = spawnPosition;

    public void SetRotation(Quaternion q) => transform.rotation = q;

    public void Activate()
    {
        IsActive = true;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        IsActive = false;
        gameObject.SetActive(false);
    }
}