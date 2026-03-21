using UnityEngine;

public class GOPanelUI : MonoBehaviour
{
    public void OnRestartButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnMainMenuButtonPressed()
    {
        GameManager.Instance.ResetGame();
    }
}
