using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += HandleScoreDisplay;
        StaminaManager.Instance.OnStaminaChanged += HandleStaminaDisplay;
        GameManager.Instance.OnGameStateChanged += ToggleGameOverDisplay;
    }

    private void HandleScoreDisplay(int newScore)
    {
        _scoreText.text = newScore.ToString("000");
    }

    private void HandleStaminaDisplay(float newStamina)
    {
        _timeText.text = newStamina.ToString("0.0");
    }

    private void ToggleGameOverDisplay(GameState state)
    {
        if (state == GameState.GameOver)
        {
            _gameOverText.gameObject.SetActive(true);
            Time.timeScale = 0.1f;
        }
        else if (state == GameState.InGame)
        {
            _gameOverText.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
