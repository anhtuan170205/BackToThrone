using UnityEngine;

public class ShopPanelUI : MonoBehaviour
{
    [SerializeField] private ShopItemUI[] _items;

    private void Start()
    {
        ShopManager.Instance.OnShopDataChanged += HandleShopDataChanged;
        ScoreManager.Instance.OnTotalScoreChanged += HandleTotalScoreChanged;
        HandleShopDataChanged();
        HandleTotalScoreChanged(ScoreManager.Instance.TotalScore);
    }

    private void OnDestroy()
    {
        ShopManager.Instance.OnShopDataChanged -= HandleShopDataChanged;
        ScoreManager.Instance.OnTotalScoreChanged -= HandleTotalScoreChanged;
    }

    private void OnEnable()
    {
        HandleShopDataChanged();
        HandleTotalScoreChanged(ScoreManager.Instance.TotalScore);
    }

    private void HandleShopDataChanged()
    {
        foreach (var item in _items)
        {
            item.Refresh();
        }
    }

    private void HandleTotalScoreChanged(int newTotalScore)
    {
        HandleShopDataChanged();
    }
}
