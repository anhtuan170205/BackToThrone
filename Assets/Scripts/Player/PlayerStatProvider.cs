using UnityEngine;

public class PlayerStatProvider : MonoBehaviour
{
    [SerializeField] private PlayerBaseStats _baseStats;

    public float MoveSpeed
    {
        get
        {
            float bonusPercent = ShopManager.Instance.GetBonus(ShopItemType.MoveSpeed);
            return _baseStats.MoveSpeed * (1f + bonusPercent);
        }
    }

    public float MaxStamina
    {
        get
        {
            return _baseStats.Stamina + ShopManager.Instance.GetBonus(ShopItemType.MaxStamina);
        }
    }

    public float StaminaBoostAmount
    {
        get
        {
            return _baseStats.StaminaBoostAmount + ShopManager.Instance.GetBonus(ShopItemType.StaminaRegen);
        }
    }

    public float ScoreMultiplier
    {
        get
        {
            return 1f + ShopManager.Instance.GetBonus(ShopItemType.ScoreMultiplier);
        }
    }

    public float PickupRange
    {
        get
        {
            return ShopManager.Instance.GetBonus(ShopItemType.PickupRange);
        }
    }

    public float JumpForce => _baseStats.JumpForce;
}
