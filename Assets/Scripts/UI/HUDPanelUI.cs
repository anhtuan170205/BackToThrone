using TMPro;
using UnityEngine;

public class HUDPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _runScoreText;
    [SerializeField] private TextMeshProUGUI _staminaText;

    private void Start()
    {
        ScoreManager.Instance.OnRunScoreChanged += HandleRunScoreChanged;
        StaminaManager.Instance.OnStaminaChanged += HandleStaminaChanged;

        HandleRunScoreChanged(ScoreManager.Instance.RunScore);
        HandleStaminaChanged(StaminaManager.Instance.CurrentStamina);
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnRunScoreChanged -= HandleRunScoreChanged;
        StaminaManager.Instance.OnStaminaChanged -= HandleStaminaChanged;
    }

    private void HandleRunScoreChanged(int newRunScore)
    {
        _runScoreText.text = newRunScore.ToString("0000");
    }

    private void HandleStaminaChanged(float newStamina)
    {
        _staminaText.text = newStamina.ToString("00.0");
    }
}
