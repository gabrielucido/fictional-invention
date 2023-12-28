using UnityEngine;

public class ProjectileLifetimeController : ProjectileBase
{
    private float _lifetime;

    void Start()
    {
        _lifetime = Projectile.data.lifetime;
    }

    private void FixedUpdate()
    {
        _lifetime -= Time.fixedDeltaTime;
        if (_lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}