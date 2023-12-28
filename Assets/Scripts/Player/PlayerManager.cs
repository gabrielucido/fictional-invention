using UnityEngine;

public class PlayerManager : MonoBehaviour, ICharacter
{
    public PlayerData data;

    // [HideInInspector]
    [Tooltip("The current player health")] public int health;

    [Tooltip("The movement input (x,y) normalized by the dead zone / snap input")]
    public Vector2 move = Vector2.zero;

    [Tooltip("True in the frame were jump was pressed down")]
    public bool jumpPressed;

    [Tooltip("Jump input has been held down for more than one frame")]
    public bool jumpHeld;

    [Tooltip("True in the frame were attack was pressed down")]
    public bool attackPressed;
    
    public bool facingRight;

    private void Start()
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
            Debug.LogError("Please assign a Player Data asset to the Player Manager data slot", this);
        }
    }
#endif

    #endregion
}