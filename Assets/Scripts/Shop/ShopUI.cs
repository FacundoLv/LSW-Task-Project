using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("Shop inventory")] 
    [SerializeField] private GameObject _shopInventoryContainer;
    [SerializeField] private ItemRow _shopItemPrefab;

    [Header("Player inventory")]
    [SerializeField] private GameObject _playerInventoryContainer;
    [SerializeField] private ItemSlot _playerItemPrefab;

    [Header("Player currency")]
    [SerializeField] private TextMeshProUGUI _playerCurrencyDisplay;

    private Inventory _shopInventory;

    private Inventory _playerInventory;
    private Currency _playerCurrency;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        var jsonFile = Resources.Load<TextAsset>("shopInventory");

        var items = JsonConvert.DeserializeObject<Item[]>(jsonFile.text);
        _shopInventory = new Inventory(items, 12);

        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Setup(Inventory playerInventory, Currency playerCurrency)
    {
        _playerInventory = playerInventory;
        _playerCurrency = playerCurrency;

        RefreshShopInventoryUI();
        RefreshPlayerInventoryUI();
        UpdatePlayerCurrency();
    }

    private void RefreshShopInventoryUI()
    {
        foreach (Transform child in _shopInventoryContainer.transform) Destroy(child.gameObject);

        foreach (var item in _shopInventory)
        {
            if (item == null) continue;
            var newItem = Instantiate(_shopItemPrefab, _shopInventoryContainer.transform);
            newItem.Set(item);
            newItem.onItemClicked += BuyItem;
        }
    }

    private void RefreshPlayerInventoryUI()
    {
        foreach (Transform child in _playerInventoryContainer.transform) Destroy(child.gameObject);

        foreach (var item in _playerInventory)
        {
            var newItem = Instantiate(_playerItemPrefab, _playerInventoryContainer.transform);
            newItem.Set(item);
            newItem.onItemClicked += SellItem;
        }
    }

    private void UpdatePlayerCurrency()
    {
        _playerCurrencyDisplay.text = _playerCurrency.Amount.ToString();
    }

    private void BuyItem(Item item)
    {
        if (!_playerCurrency.CanSpend(item.Price) || !_playerInventory.CanAdd()) return;

        _playerCurrency.Spend(item.Price);
        _playerInventory.Add(item);
        _shopInventory.Remove(item);
        
        RefreshShopInventoryUI();
        RefreshPlayerInventoryUI();
        UpdatePlayerCurrency();
    }

    private void SellItem(ItemSlot slot, Item item)
    {
        if (!_playerInventory.HasInInventory(item)) return;

        _playerCurrency.Add(item.Price);
        _playerInventory.Remove(item);
        _shopInventory.Add(item);
        
        RefreshShopInventoryUI();
        RefreshPlayerInventoryUI();
        UpdatePlayerCurrency();
    }
}