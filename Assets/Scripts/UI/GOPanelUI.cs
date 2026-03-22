using UnityEngine;

public class GOPanelUI : MonoBehaviour
{
    public void OnMainMenuButtonPressed()
    {
        GameManager.Instance.ResetGame();
    }
}
