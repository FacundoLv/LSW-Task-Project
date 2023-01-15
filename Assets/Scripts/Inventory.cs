using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IEnumerable<Item>
{
    private readonly Item[] _items;

    public Inventory(Item[] items, int size)
    {
        _items = new Item[size];
        items.CopyTo(_items, 0);
    }

    public Inventory(InventoryData data)
    {
        _items = new Item[data.Size];
        data.Items.CopyTo(_items, 0);
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

    public IEnumerator<Item> GetEnumerator()
    {
        for (var i = 0; i < _items.Length; i++) yield return _items[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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

[Serializable]
public class InventoryData
{
    public Item[] Items;
    public int Size;
}