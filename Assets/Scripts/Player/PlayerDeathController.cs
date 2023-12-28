using System;
using UnityEngine;

public class PlayerDeathController : PlayerBase
{
    public event Action<GameObject> Died;

    private void FixedUpdate()
    {
        HandleDeathCondition();
    }

    private void HandleDeathCondition()
    {
        if (Player.health <= 0)
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