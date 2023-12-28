using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ProjectileMovementController : ProjectileBase
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var velocity = new Vector2((Projectile.direction.x * Projectile.data.speed),
            _rb.velocity.y);
        _rb.velocity = velocity;
    }
}