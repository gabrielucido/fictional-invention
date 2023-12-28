using UnityEngine;

/// <summary>
/// Base class for all enemy controllers 
/// </summary>
/// <remarks>
/// This automatically sets a reference to the main EnemyManager script to be used within a controller.
///  For example to access the enemy's data scriptable object, you can use EnemyController.enemyData
/// </remarks>
public abstract class EnemyBase : MonoBehaviour
{
    protected EnemyManager Instance;

    protected virtual void Awake()
    {
        Instance = GetComponentInParent<EnemyManager>();
    }
}