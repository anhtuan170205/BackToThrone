using System;
using UnityEngine;

[Serializable]
public class ShopUpgradeData
{
    [Tooltip("The type of upgrade this data represents.")]
    [SerializeField] private ShopItemType _item;

    [Tooltip("Current level of the upgrade.")]
    [SerializeField] private int _level;

    [Tooltip("Maximum level for this upgrade.")]
    [SerializeField] private int _maxLevel;

    [Header("Cost")]
    [Tooltip("Base cost of the upgrade.")]
    [SerializeField] private int _baseCost = 200;

    [Tooltip("Additional cost per level.")]
    [SerializeField] private int _costMultiplier = 10;

    [Header("Value")]
    [Tooltip("The bonus value provided by each level of this upgrade.")]
    [SerializeField] private float _valuePerLevel = 1f;

    public ShopItemType Item => _item;
    public int Level => _level; 
    public int MaxLevel => _maxLevel;
    public bool IsMaxLevel => _level >= _maxLevel;
    public float ValuePerLevel => _valuePerLevel;
    public int CurrentCost => _baseCost + (_level * _costMultiplier);
    public float TotalBonus => _level * _valuePerLevel;

    public bool TryUpgrade()
    {
        if (IsMaxLevel) { return false; }

        _level++;
        return true;
    }

}
