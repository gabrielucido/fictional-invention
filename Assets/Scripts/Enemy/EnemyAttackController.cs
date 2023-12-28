using System;
using UnityEngine;

public class EnemyAttackController : PlayerBase
{
    private Rigidbody2D _rb;
    private float _time;
    private float _attackCooldown = 0f;
    public GameObject projectile;
    public event Action Attacked;

    private void Update()
    {
        _time += Time.deltaTime;
        HandleAttackTimer();
    }

    void FixedUpdate()
    {
        HandleAttack();
    }


    #region Attack

    private void HandleAttackTimer()
    {
        if (_attackCooldown > 0)
        {
            _attackCooldown -= _time;
        }
        else
        {
            _attackCooldown = 0;
        }
    }

    private void HandleAttack()
    {
        if (Player.attackPressed && _attackCooldown == 0)
        {
            _attackCooldown = Player.data.attackCooldown;
            ExecuteAttack();
        }

        Player.attackPressed = false;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ExecuteAttack()
    {
        var facingRight = true;
        var auxSpawnPosition = transform.position;
        auxSpawnPosition.x = auxSpawnPosition.x + (facingRight ? 1 : -1);
        var instance =
            Instantiate(projectile, auxSpawnPosition, transform.rotation);
        instance.GetComponent<ProjectileManager>().direction = new Vector2(facingRight ? 1 : -1, 0);
        Attacked?.Invoke();
    }

    #endregion
}