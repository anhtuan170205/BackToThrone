using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerStatProvider _playerStatProvider;
    [SerializeField] private CapsuleCollider _pickUpCollider;
    public event Action OnCollideWithHazard;
    public event Action OnCollideWithObstacle;

    private void Start()
    {
        ShopManager.Instance.OnUpgradePurchased += HandleUpgradePurchased;
    }

    private void OnDestroy() 
    {
        ShopManager.Instance.OnUpgradePurchased -= HandleUpgradePurchased;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) { return; }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            OnCollideWithObstacle?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (GameManager.Instance.CurrentGameState != GameState.InGame) { return; }
        
        if (other.gameObject.CompareTag("Hazard"))
        {
            OnCollideWithHazard?.Invoke();
        }
    }

    private void HandleUpgradePurchased(ShopItemType item)
    {
        if (item == ShopItemType.PickupRange)
        {
            _pickUpCollider.radius = _playerStatProvider.PickupRange;
        }
    }
}
