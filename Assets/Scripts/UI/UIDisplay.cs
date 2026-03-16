using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay(int newScore)
    {
        _scoreText.text = "Score: " + newScore.ToString("000");
    }
}
