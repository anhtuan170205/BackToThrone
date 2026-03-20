using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;
        Debug.Log("Checkpoint reached!");

        PlayerStatProvider playerStatProvider = other.GetComponent<PlayerStatProvider>();
        if (playerStatProvider == null) return;

        StaminaManager.Instance.AddStamina(playerStatProvider.StaminaBoostAmount);
        LevelGenerator.Instance.IncreaseDifficulty();
    }
}
