using Newtonsoft.Json;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Inventory _shopInventory;

    private Inventory _playerInventory;
    private Currency _playerCurrency;

    private void Start()
    {
        var jsonFile = Resources.Load<TextAsset>("shopInventory");

        var itemList = JsonConvert.DeserializeObject<Item[]>(jsonFile.text);
        _shopInventory = new Inventory(itemList, 12);
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
}