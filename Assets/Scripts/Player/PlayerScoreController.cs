using TMPro;
using UnityEngine;

public class PlayerScoreController : PlayerBase
{
    public int score = 0;
    public GameObject text;
    private TMP_Text _scoreText;


    private void Start()
    {
        _scoreText = text.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _scoreText.SetText(score.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectible = other.gameObject.GetComponent<ICollectible>();
        collectible?.Collect(gameObject);
        if (collectible != null)
        {
            score++;
        }
    }


    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (text == null)
        {
            Debug.LogError("Please assign the Text Component to the Player Score Controller Text slot",
                this);
        }
    }
#endif

    #endregion
}