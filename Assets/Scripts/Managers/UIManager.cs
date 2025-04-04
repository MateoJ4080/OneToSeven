using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayScore;
    [SerializeField] private TextMeshProUGUI displayHealth;

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealth _playerHealth;

    void OnEnable()
    {
        StartCoroutine(WaitForPlayerToUpdateUI());
    }

    void OnDisable()
    {
        // Unsubscribe to avoid null references and improve performance when the object is disabled
        ScoreManager.OnScoreChanged -= UpdateScoreText;
        _playerHealth.OnHealthChanged -= UpdateHealthText;
    }

    private IEnumerator WaitForPlayerToUpdateUI()
    {
        while (_playerHealth == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerHealth = player.GetComponent<PlayerHealth>();

                ScoreManager.OnScoreChanged += UpdateScoreText;
                _playerHealth.OnHealthChanged += UpdateHealthText;
            }

            yield return null;
        }
    }

    // Updates the UI only when text values change
    private void UpdateScoreText()
    {
        displayScore.text = "Score: " + ScoreManager.Score.ToString();
    }
    private void UpdateHealthText()
    {
        displayHealth.text = "Health: " + _playerHealth.Health.ToString();
    }
}
