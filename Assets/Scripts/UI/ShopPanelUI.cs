using UnityEngine;

public class ShopPanelUI : MonoBehaviour
{
    [SerializeField] private ShopItemUI[] _items;

    private void Start()
    {
        ShopManager.Instance.OnShopDataChanged += HandleShopDataChanged;
        HandleShopDataChanged();
    }

    private void OnDestroy()
    {
        ShopManager.Instance.OnShopDataChanged -= HandleShopDataChanged;
    }

    private void OnEnable()
    {
        HandleShopDataChanged();
    }

    private void HandleShopDataChanged()
    {
        foreach (var item in _items)
        {
            item.Refresh();
        }
    }
}
