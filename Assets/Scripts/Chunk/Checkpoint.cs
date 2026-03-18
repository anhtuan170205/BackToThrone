using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private readonly string PLAYER_TAG = "Player";
    [SerializeField] private PlayerStats _playerStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            StaminaManager.Instance.AddStamina(_playerStats.StaminaBoostAmount);
            LevelGenerator.Instance.IncreaseDifficulty();
        }
    }
}
