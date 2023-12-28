using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ProjectileCollisionController : ProjectileBase
{
    /// <summary>
    /// CollidedWithCharacter is invoked when the projectile collides with a character
    /// </summary>
    /// <param index="0">The GameObject that originated the projectile</param>
    public event Action<GameObject> CollidedWithCharacter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hit = other.GetComponentInChildren<ICharacter>();
        if (hit == null) return;
        if (Projectile.shotBy == other.gameObject) return;

        hit?.TakeDamage(Projectile.data.damage);
        CollidedWithCharacter?.Invoke(Projectile.shotBy ? Projectile.shotBy : null);
        DestroyMe();
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}