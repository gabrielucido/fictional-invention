using UnityEngine;

/// <summary>
/// Base class for all player controllers 
/// </summary>
/// <remarks>
/// This automatically sets a reference to the main PlayerManager script to be used within a controller.
///  For example to access the player's data scriptable object, you can use PlayerController.playerData
/// </remarks>
public abstract class PlayerBase : MonoBehaviour
{
    protected PlayerManager Player;
    // protected Rigidbody2D _rb;
    // protected CapsuleCollider2D _col;

    protected virtual void Awake()
    {
        Player = GetComponentInParent<PlayerManager>();
        // _rb = GetComponent<Rigidbody2D>();
        // _col = GetComponent<CapsuleCollider2D>();
    }
}