using UnityEngine;

public class Coin : Pickup
{
    [SerializeField] private int _scoreValue = 10;
    protected override void OnPickup()
    {
        ScoreManager.Instance.AddScore(_scoreValue);
        AudioManager.Instance.PlayCoinPickupSfx();
        Destroy(gameObject);
    }
}
