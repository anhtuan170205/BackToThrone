using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    [SerializeField] private ShopUpgradeDataSO[] _upgrades;

    private Dictionary<ShopItemType, ShopUpgradeDataSO> _upgradeDictionary;
    public event Action<ShopItemType> OnUpgradePurchased;
    public event Action OnShopDataChanged;

    protected override void Awake()
    {
        base.Awake();

        _upgradeDictionary = new Dictionary<ShopItemType, ShopUpgradeDataSO>();
        foreach (var upgrade in _upgrades)
        {
            _upgradeDictionary.Add(upgrade.Item, upgrade);
        }
    }

    public int GetLevel(ShopItemType item)
    {
        if (!_upgradeDictionary.TryGetValue(item, out var data)) { return 0; }
        return data.Level;
    }

    public int GetCost(ShopItemType item)
    {
        if (!_upgradeDictionary.TryGetValue(item, out var data)) { return 0; }
        return data.CurrentCost;
    }

    public float GetBonus(ShopItemType item)
    {
        if (!_upgradeDictionary.TryGetValue(item, out var data)) { return 0f; }
        return data.TotalBonus;
    }

    public bool IsMaxLevel(ShopItemType item)
    {
        if (!_upgradeDictionary.TryGetValue(item, out var data)) { return true; }
        return data.IsMaxLevel;
    }

    public string GetItemName(ShopItemType item)
    {
        if (!_upgradeDictionary.TryGetValue(item, out var data)) { return ""; }
        return data.ItemName;
    }

    public bool TryBuy(ShopItemType item)
    {
        if (!_upgradeDictionary.TryGetValue(item, out var data)) { return false; }
        if (data.IsMaxLevel) { return false; }

        int cost = data.CurrentCost;
        if (!ScoreManager.Instance.TrySpendTotalScore(cost)) { return false; }

        data.TryUpgrade();
        OnUpgradePurchased?.Invoke(item);
        OnShopDataChanged?.Invoke();
        
        return true;
    }
}
