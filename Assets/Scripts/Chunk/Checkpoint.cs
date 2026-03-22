using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;

        StaminaManager.Instance.AddStamina(5f);
        LevelGenerator.Instance.IncreaseDifficulty();
    }
}
