using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider2D))]
public class EnemyRangeController : EnemyBase
{
    [SerializeField] private string targetTag = "Player";

    private float _distanceToPlayer;
    private float _targetDistanceOffset;
    private bool _targetInRange;
    private bool _obstaclesBetweenPlayer;
    private bool _engagingPlayer;

    private void Update()
    {
        HandleFollowPlayer();
        ExecuteFollowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            StartFollowingTarget(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            StopFollowingTarget();
        }
    }

    private void StartFollowingTarget(GameObject followTarget)
    {
        Instance.followTarget = followTarget;
        _targetInRange = true;
        _targetDistanceOffset = Random.Range(0.4f, 1.5f);
    }

    private void StopFollowingTarget()
    {
        Instance.followTarget = null;
        _targetInRange = false;
    }

    private void HandleFollowPlayer()
    {
        if (!_targetInRange) return;
        // create the raycast
        var localTransform = transform;
        var position = localTransform.position;
        var playerPosition = Instance.followTarget.transform.position;
        var boxCastSize = new Vector2(.1f, localTransform.localScale.y - .2f);
        var deltaX = position.x - playerPosition.x;
        var hit = Physics2D.BoxCast(position, boxCastSize, 0f,
            deltaX < 0 ? Vector2.right : Vector2.left,
            100f, ~LayerMask.GetMask("Enemy", "Ignore Raycast"));

        if (hit.collider)
        {
            _obstaclesBetweenPlayer = !hit.collider.CompareTag("Player");
        }
    }

    private void ExecuteFollowPlayer()
    {
        if (!_targetInRange) return;
        _distanceToPlayer = Vector3.Distance(transform.position, Instance.followTarget.transform.position);
        _engagingPlayer = _targetInRange && !_obstaclesBetweenPlayer &&
                          _distanceToPlayer > (Instance.data.followDistance + _targetDistanceOffset);
        if (!_engagingPlayer) return;
        var localTransform = transform;
        var position = localTransform.position;
        var playerPosition = Instance.followTarget.transform.position;
        var deltaX = position.x - playerPosition.x;
        var direction = deltaX < 0 ? Vector2.right : Vector2.left;
        var velocity = direction * (Instance.data.speed * Time.fixedDeltaTime);
        localTransform.Translate(velocity);
    }
}