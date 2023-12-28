using System;
using UnityEngine;

public class EnemyDeathController : EnemyBase
{
    public event Action<GameObject> Died;

    private void FixedUpdate()
    {
        HandleDeathCondition();
    }

    private void HandleDeathCondition()
    {
        if (Instance.health <= 0)
        {
            ExecuteDeath();
        }
    }

    private void ExecuteDeath()
    {
        Died?.Invoke(gameObject);
        Destroy(gameObject);
    }
}