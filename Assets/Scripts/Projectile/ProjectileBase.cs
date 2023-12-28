using UnityEngine;

/// <summary>
/// Base class for all projectile controllers 
/// </summary>
/// <remarks>
/// This automatically sets a reference to the main PlayerManager script to be used within a controller.
///  For example to access the player's data scriptable object, you can use PlayerController.playerData
/// </remarks>
public abstract class ProjectileBase : MonoBehaviour
{
    protected ProjectileManager Projectile;

    protected virtual void Awake()
    {
        Projectile = GetComponentInParent<ProjectileManager>();
    }
}