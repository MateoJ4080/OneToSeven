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
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _playerShooting = _player.GetComponent<PlayerShooting>();

        UpdateScoreText();
        UpdateHealthText();
        UpdateBulletsText();
    }

    void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreText;
        _playerHealth.OnHealthChanged += UpdateHealthText;
        _playerShooting.OnBulletShoot += UpdateBulletsText;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid null references and improve performance when the object is disabled
        ScoreManager.OnScoreChanged -= UpdateScoreText;
        _playerHealth.OnHealthChanged -= UpdateHealthText;
        _playerShooting.OnBulletShoot -= UpdateBulletsText;
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
        displayBulletsShot.text = "Bullets: " + _playerShooting.bulletsShot.ToString();
    }
}
