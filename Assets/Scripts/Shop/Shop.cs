using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour, IInteractable
{
    public UnityEvent onFocused;
    public UnityEvent onUnfocused;

    [SerializeField] private ShopUI _ui;
    
    private Inventory _shopInventory;

    private Inventory _playerInventory;
    private Currency _playerCurrency;

    private void Start()
    {
        var jsonFile = Resources.Load<TextAsset>("shopInventory");

        var items = JsonConvert.DeserializeObject<Item[]>(jsonFile.text);
        _shopInventory = new Inventory(items, 12);
    }

    public void BuyItem(Item item)
    {
        if (_playerCurrency.CanSpend(item.Price) && _playerInventory.CanAddToInventory(item))
        {
            _playerCurrency.Spend(item.Price);
            _playerInventory.Add(item);
            _shopInventory.Remove(item);
        }
    }

    public void SellItem(Item item)
    {
        if (_playerInventory.HasInInventory(item))
        {
            _playerCurrency.Add(item.Price);
            _playerInventory.Remove(item);
            _shopInventory.Add(item);
        }
    }

    public void OnFocused()
    {
        onFocused?.Invoke();
    }

    public void OnUnfocused()
    {
        onUnfocused?.Invoke();
    }

    public void Interact<T>(T interactor) where T : MonoBehaviour, IInteractor
    {
        var player = interactor.GetComponent<PlayerController>();
        if (player == null) return;

        _playerCurrency = player.GetCurrency();
        _playerInventory = player.GetInventory();

        _ui.Setup(_shopInventory, _playerInventory, _playerCurrency);
        _ui.Open();
    }
}