using TMPro;
using UnityEngine;

public class MenuPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private TextMeshProUGUI _totalScoreText;

    private void Start()
    {
        ScoreManager.Instance.OnTotalScoreChanged += HandleTotalScoreChanged;
        HandleTotalScoreChanged(ScoreManager.Instance.TotalScore);
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnTotalScoreChanged -= HandleTotalScoreChanged;
    }

    private void HandleTotalScoreChanged(int newTotalScore)
    {
        _totalScoreText.text = newTotalScore.ToString("0000");
    }

    public void OnStartButtonPressed()
    {
        GameManager.Instance.StartGame();
    }

    public void OnShopButtonPressed()
    {
        _shopPanel.SetActive(true);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
