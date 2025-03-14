using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayScore;
    [SerializeField] private TextMeshProUGUI displayHealth;
    [SerializeField] private TextMeshProUGUI displayBulletsShot;

    [SerializeField] private GameObject _player;
    private PlayerHealth _playerHealth;
    private PlayerShooting _playerShooting;

    void Awake()
    {
    }

    void OnEnable()
    {
        StartCoroutine(WaitForPlayerToUpdateUI());
    }

    void OnDisable()
    {
        // Unsubscribe to avoid null references and improve performance when the object is disabled
        ScoreManager.OnScoreChanged -= UpdateScoreText;
        _playerHealth.OnHealthChanged -= UpdateHealthText;
        _playerShooting.OnBulletShoot -= UpdateBulletsText;
    }

    private IEnumerator WaitForPlayerToUpdateUI()
    {
        while (_playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerHealth = player.GetComponent<PlayerHealth>();
                _playerShooting = player.GetComponent<PlayerShooting>();

                ScoreManager.OnScoreChanged += UpdateScoreText;
                _playerHealth.OnHealthChanged += UpdateHealthText;
                _playerShooting.OnBulletShoot += UpdateBulletsText;
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
    private void UpdateBulletsText()
    {
        displayBulletsShot.text = "Bullets: " + _playerShooting.BulletsShot.ToString();
    }
}
