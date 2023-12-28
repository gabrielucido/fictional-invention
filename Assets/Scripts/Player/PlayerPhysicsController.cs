using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerPhysicsController : PlayerBase
{
    private CapsuleCollider2D _col;

    /// <summary>
    /// Invoked whenever a player hits or leaves the ground 
    /// </summary>
    /// <param>True when player is grounded, false when leaving the ground</param>
    public event Action<bool> GroundedChanged;

    /// <summary>
    /// Invoked whenever a player hits the ceiling
    /// </summary>
    public event Action HitCeiling;

    private bool _grounded;
    private bool _cachedQueryStartInColliders;

    void Start()
    {
        _col = Player.GetComponent<CapsuleCollider2D>();
        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        _grounded = true;
    }

    void FixedUpdate()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down,
            Player.data.grounderDistance, ~Player.data.playerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up,
            Player.data.grounderDistance, ~Player.data.playerLayer);

        // Hit a Ceiling
        if (ceilingHit)
        {
            HitCeiling?.Invoke();
        }

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            GroundedChanged?.Invoke(true);
        }

        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            GroundedChanged?.Invoke(false);
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }
}