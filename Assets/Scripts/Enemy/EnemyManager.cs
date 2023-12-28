using UnityEngine;

public class EnemyManager : MonoBehaviour, ICharacter
{
    public EnemyData data;
    public GameObject followTarget;

    public int health;

    public void Start()
    {
        health = data.maxHealthPoints;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (data == null)
        {
            Debug.LogError("Please assign a Enemy Data asset to the Enemy Manager data slot", this);
        }
    }
#endif

    #endregion
}