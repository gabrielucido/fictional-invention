using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerAnimationController : PlayerBase
{
    private PlayerMovementController _movementController;
    private PlayerPhysicsController _physicsController;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _grounded = true;
    private bool _walking;
    private bool _idling;

    protected override void Awake()
    {
        base.Awake();
        _movementController = GetComponent<PlayerMovementController>();
        _physicsController = GetComponent<PlayerPhysicsController>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        HandleSpriteFlip();
        HandleAnimationState();
    }

    private void HandleAnimationState()
    {
        var velocity = _movementController.GetVelocity();
        _animator.SetBool(IdlingKey, Player.move.x == 0);
        _animator.SetBool(WalkingKey, Player.move.x != 0);

        _animator.SetBool(Falling, velocity.y < 0 && !_grounded);
        _animator.SetBool(Jumping, velocity.y > 0 && !_grounded);
    }

    private void HandleSpriteFlip()
    {
        if (Player.move.x != 0) _spriteRenderer.flipX = Player.move.x < 0;
        Player.facingRight = !_spriteRenderer.flipX;
    }

    // private void OnAttacked()
    // {
    //     var facingRight = _playerManager.IsFacingRight();
    //     var auxSpawnPosition = transform.position;
    //     auxSpawnPosition.x = auxSpawnPosition.x + (facingRight ? 1 : -1);
    //     var instance =
    //         Instantiate(projectile, auxSpawnPosition, transform.rotation); //, auxSpawnPosition, Quaternion.identity);
    //     instance.GetComponent<ProjectileController>().goingLeft = !facingRight;
    // }

    #region Events

    private void OnEnable()
    {
        _physicsController.GroundedChanged += OnGroundChanged;
        // Player.Jumped += OnJumped;
        // _playerManager.Attacked += OnAttacked;
        // _player.GroundedChanged += OnGroundedChanged;
        //
        // _moveParticles.Play();
    }

    private void OnDisable()
    {
        _physicsController.GroundedChanged -= OnGroundChanged;
        // _playerManager.Attacked -= OnAttacked;
        // _player.GroundedChanged -= OnGroundedChanged;
        // _moveParticles.Stop();
    }

    #endregion

    private void OnGroundChanged(bool grounded)
    {
        _grounded = grounded;
    }

    private static readonly int IdlingKey = Animator.StringToHash("idling");
    private static readonly int WalkingKey = Animator.StringToHash("walking");
    private static readonly int Jumping = Animator.StringToHash("jumping");
    private static readonly int Falling = Animator.StringToHash("falling");
}