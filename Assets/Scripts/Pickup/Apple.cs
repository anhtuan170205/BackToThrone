using UnityEngine;

public class Apple : Pickup
{
    protected override void OnPickup()
    {
        LevelGenerator.Instance.ChangeLevelSpeed(2f);
        AudioManager.Instance.PlaySpeedUpSfx();
        Destroy(gameObject);
    }
}
