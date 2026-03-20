using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _runScoreText;
    [SerializeField] private TextMeshProUGUI _timeText;

    [SerializeField] private TextMeshProUGUI _menuTotalScoreText;

    private void Start()
    {
        ScoreManager.Instance.OnRunScoreChanged += HandleRunScoreDisplay;
        ScoreManager.Instance.OnTotalScoreChanged += HandleTotalScoreDisplay;
        StaminaManager.Instance.OnStaminaChanged += HandleStaminaDisplay;
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnRunScoreChanged -= HandleRunScoreDisplay;
        ScoreManager.Instance.OnTotalScoreChanged -= HandleTotalScoreDisplay;
        StaminaManager.Instance.OnStaminaChanged -= HandleStaminaDisplay;
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleRunScoreDisplay(int newRunScore)
    {
        _runScoreText.text = newRunScore.ToString("000");
    }

    private void HandleTotalScoreDisplay(int newTotalScore)
    {
        _menuTotalScoreText.text = newTotalScore.ToString("000");
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
                _gameOverPanel.SetActive(false);
                Time.timeScale = 1f;
                break;

            case GameState.InGame:
                _menuPanel.SetActive(false);
                _hudPanel.SetActive(true);
                _gameOverPanel.SetActive(false);
                Time.timeScale = 1f;
                break;

            case GameState.GameOver:
                _menuPanel.SetActive(false);
                _hudPanel.SetActive(false);
                _gameOverPanel.SetActive(true);
                Time.timeScale = 0.1f;
                break;
        }
    }

    public void OnStartButtonPressed()
    {
        GameManager.Instance.StartGame();
    }
}
