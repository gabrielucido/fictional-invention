using System;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysicsController))]
public class PlayerMovementController : PlayerBase
{
    private Rigidbody2D _rb;
    private PlayerPhysicsController _playerPhysicsController;
    private Vector2 _frameVelocity;
    private bool _grounded;
    private float _time;

    public event Action Jumped;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _playerPhysicsController = GetComponent<PlayerPhysicsController>();
        _grounded = true;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        HandleJumpTimer();
    }

    void FixedUpdate()
    {
        HandleJump();
        HandleDirection();
        HandleGravity();
        ApplyMovement();
    }

    private void OnEnable()
    {
        _playerPhysicsController.GroundedChanged += OnGroundedChanged;
        _playerPhysicsController.HitCeiling += OnHitCeiling;
    }
    
    private void OnDisable()
    {
        _playerPhysicsController.GroundedChanged -= OnGroundedChanged;
        _playerPhysicsController.HitCeiling -= OnHitCeiling;
    }

    #region Horizontal

    private void HandleDirection()
    {
        if (Player.move.x == 0)
        {
            var deceleration = _grounded
                ? Player.data.groundDeceleration
                : Player.data.airDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x,
                Player.move.x * Player.data.maxSpeed,
                Player.data.acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Jump

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;

    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _jumpPressedTime;     
    private float _frameLeftGrounded;

    private bool HasBufferedJump =>
        _bufferedJumpUsable && _time < _jumpPressedTime + Player.data.jumpBuffer;

    private bool CanUseCoyote =>
        _coyoteUsable && !_grounded && _time < _frameLeftGrounded + Player.data.coyoteTime;

    private void HandleJumpTimer()
    {
        if (!Player.jumpPressed) return;
        _jumpPressedTime = _time;
        _jumpToConsume = true;
    }

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !Player.jumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();
        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _jumpPressedTime = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = Player.data.jumpPower;
        Jumped?.Invoke();
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = Player.data.groundingForce;
        }
        else
        {
            var inAirGravity = Player.data.fallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0)
                inAirGravity *= Player.data.jumpEndEarlyGravityModifier;
            _frameVelocity.y =
                Mathf.MoveTowards(_frameVelocity.y, -Player.data.maxFallSpeed,
                    inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Events

    private void OnGroundedChanged(bool grounded)
    {
        switch (_grounded)
        {
            case false when grounded:
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                break;
            case true when !grounded:
                _grounded = false;
                _frameLeftGrounded = _time;
                break;
        }
    }

    private void OnHitCeiling()
    {
        _frameVelocity.y = 0;
    }

    #endregion


    private void ApplyMovement()
    {
        _rb.velocity = _frameVelocity;
    }

    public Vector2 GetVelocity()
    {
        return _rb.velocity;
    }
}