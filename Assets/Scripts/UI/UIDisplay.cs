using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += HandleScoreDisplay;
        StaminaManager.Instance.OnStaminaChanged += HandleStaminaDisplay;
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreChanged -= HandleScoreDisplay;
        StaminaManager.Instance.OnStaminaChanged -= HandleStaminaDisplay;
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleScoreDisplay(int newScore)
    {
        _scoreText.text = newScore.ToString("000");
    }

    private void HandleStaminaDisplay(float newStamina)
    {
        _timeText.text = newStamina.ToString("0.0");
    }

    private void HandleGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                _menuPanel.SetActive(true);
                _hudPanel.SetActive(false);
                _gameOverText.gameObject.SetActive(false);
                Time.timeScale = 1f;
                break;

            case GameState.InGame:
                _menuPanel.SetActive(false);
                _hudPanel.SetActive(true);
                _gameOverText.gameObject.SetActive(false);
                Time.timeScale = 1f;
                break;

            case GameState.GameOver:
                _menuPanel.SetActive(false);
                _hudPanel.SetActive(true);
                _gameOverText.gameObject.SetActive(true);
                Time.timeScale = 0.1f;
                break;
        }
    }

    public void OnStartButtonPressed()
    {
        GameManager.Instance.StartGame();
    }
}
