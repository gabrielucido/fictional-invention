using UnityEngine;

[CreateAssetMenu]
public class ProjectileData : ScriptableObject
{
    [Header("General")] [Tooltip("The movement speed of the projectile"), Range(1, 100)]
    public float speed = 100;

    [Tooltip("The damage if it hits something"), Range(1, 1000)]
    public int damage = 30;

    [Tooltip("The lifetime before the projectile destroy itself"), Range(1, 60)]
    public float lifetime = 10;
}