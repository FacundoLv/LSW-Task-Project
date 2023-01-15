using System;
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
    [SerializeField] private TextMeshProUGUI _playerCurrency;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Setup(Inventory shopInventory, Inventory playerInventory, Currency playerCurrency)
    {
        foreach (Transform child in _shopInventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in shopInventory)
        {
            if (item == null) continue;
            var newItem = Instantiate(_shopItemPrefab, _shopInventoryContainer.transform);
            newItem.SetRow(item);
        }
        
        foreach (Transform child in _playerInventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in playerInventory)
        {
            var newItem = Instantiate(_playerItemPrefab, _playerInventoryContainer.transform);
        }

        _playerCurrency.text = playerCurrency.Amount.ToString();
    }
}