using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _hudPanel;
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }


    private void HandleGameStateChanged(GameState state)
    {
        _menuPanel.SetActive(state == GameState.MainMenu);
        _hudPanel.SetActive(state == GameState.InGame);
        _gameOverPanel.SetActive(state == GameState.GameOver);
    }

    public void OnStartButtonPressed()
    {
        GameManager.Instance.StartGame();
    }
}
