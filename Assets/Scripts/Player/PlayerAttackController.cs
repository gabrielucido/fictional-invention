using System;
using UnityEngine;

public class PlayerAttackController : PlayerBase
{
    private Rigidbody2D _rb;

    private float _time;
    private float _cooldown = 0;

    [SerializeField] private Vector3 muzzleFlashOffset = Vector3.zero;

    public GameObject projectile;
    public event Action Attacked;

    private void Update()
    {
        _time += Time.deltaTime;
        HandleAttackTimer();
        HandleAttack();
    }


    #region Attack

    private void HandleAttackTimer()
    {
        if (_cooldown > 0)
        {
            _cooldown -= _time;
        }
        else
        {
            _cooldown = 0;
        }
    }

    private void HandleAttack()
    {
        if (Player.attackPressed && _cooldown == 0)
        {
            _cooldown = Player.data.attackCooldown;
            ExecuteAttack();
        }

        Player.attackPressed = false;
    }

    private void ExecuteAttack()
    {
        var auxSpawnPosition = transform.position;
        auxSpawnPosition.x = auxSpawnPosition.x + (Player.facingRight ? 1 : -1);
        auxSpawnPosition += muzzleFlashOffset;
        var instance =
            Instantiate(projectile, auxSpawnPosition, transform.rotation);
        var projectileManager = instance.GetComponent<ProjectileManager>();
        projectileManager.direction = new Vector2(Player.facingRight ? 1 : -1, 0);
        projectileManager.shotBy = gameObject;
        Attacked?.Invoke();
    }

    #endregion

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (projectile == null)
        {
            Debug.LogError("Please assign a Projectile GameObject Prefab to the Player Attack projectile slot", this);
        }
    }
#endif

    #endregion
}