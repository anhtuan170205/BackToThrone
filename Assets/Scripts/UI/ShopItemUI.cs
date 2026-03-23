using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private ShopItemType _itemType;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _buyButtonText;

    private void Start()
    {
        _buyButton.onClick.AddListener(OnBuyButtonPressed);
        Refresh();
    }

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        int level = ShopManager.Instance.GetLevel(_itemType);
        int cost = ShopManager.Instance.GetCost(_itemType);
        float bonus = ShopManager.Instance.GetBonus(_itemType);
        bool isMaxLevel = ShopManager.Instance.IsMaxLevel(_itemType);
        string itemName = ShopManager.Instance.GetItemName(_itemType);
        int totalScore = ScoreManager.Instance.TotalScore;

        _levelText.text = $"Lv. {level}";
        _nameText.text = itemName;

        if (isMaxLevel)
        {
            _costText.text = "MAX";
            _buyButton.interactable = false;
            _buyButtonText.text = "MAX";
        }
        else
        {
            _costText.text = cost.ToString();
            _buyButton.interactable = totalScore >= cost;
            _buyButtonText.text = "BUY";
        }
    }

    public void OnBuyButtonPressed()
    {
        if (ShopManager.Instance.TryBuy(_itemType))
        {
            Refresh();
        }
    }
}
