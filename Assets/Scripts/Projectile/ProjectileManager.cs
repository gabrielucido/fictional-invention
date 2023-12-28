using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public ProjectileData data;

    [Tooltip("The direction that the projectile travels")]
    public Vector2 direction = Vector2.right;

    [Tooltip("The GameObject responsible for the projectile")]
    public GameObject shotBy;

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (data == null)
        {
            Debug.LogError("Please assign a Projectile Data asset to the Projectile Manager data slot", this);
        }
    }
#endif

    #endregion
}