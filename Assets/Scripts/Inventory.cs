using System;
using UnityEngine;

public class Inventory
{
    private readonly Item[] _items;

    public Inventory(Item[] itemList, int size)
    {
        _items = new Item[size];
        itemList.CopyTo(_items, 0);
    }

    public void Add(Item item)
    {
        if (CanAddToInventory(item))
        {
            var index = Array.FindIndex(_items, n => n == null);
            _items[index] = item;
            return;
        }

        Debug.Log("Inventory is full.");
    }

    public void Remove(Item item)
    {
        if (HasInInventory(item))
        {
            var index = Array.FindIndex(_items, n => n.Equals(item));
            _items[index] = null;
            return;
        }

        Debug.Log("Item not found in inventory.");
    }

    public bool HasInInventory(Item item)
    {
        return Array.Exists(_items, n => n.Equals(item));
    }

    public bool CanAddToInventory(Item item)
    {
        return Array.Exists(_items, n => n == null);
    }
}