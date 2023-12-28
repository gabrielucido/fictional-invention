using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : EnemyBase
{
    public Transform waypointA;
    public Transform waypointB;


    private Transform _target;
    private Rigidbody2D _rb;

    private void Start()
    {
        _target = waypointA;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleCurrentTarget()
    {
        if (Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            _target = _target == waypointA ? waypointB : waypointA;
        }
    }

    void HandleMovement()
    {
        if (Instance.followTarget == null)
        {
            HandleCurrentTarget();
            Vector2 direction = (_target.position - transform.position).normalized;
            _rb.velocity = new Vector2(direction.x * Instance.data.patrolSpeed, _rb.velocity.y);
        }
    }
}