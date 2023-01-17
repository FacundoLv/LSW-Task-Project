using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : IEnumerable<Item>
{
    public event Action<Item> onItemAdded;
    public event Action<Item> onItemRemoved;
    
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
        if (CanAdd())
        {
            var index = Array.FindIndex(_items, n => n == null);
            _items[index] = item;
            onItemAdded?.Invoke(item);
            return;
        }

        Debug.Log("Inventory is full.");
    }

    public void Remove(Item item)
    {
        if (HasInInventory(item))
        {
            var index = Array.FindIndex(_items, n => n != null && n.Equals(item));
            _items[index] = null;
            onItemRemoved?.Invoke(item);
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
        return Array.Exists(_items, n => n != null && n.Equals(item));
    }

    public bool CanAdd()
    {
        return Array.Exists(_items, n => n == null);
    }
}

[Serializable]
public class InventoryData
{
    public Item[] Equipped;
    public Item[] Items;
    public int Size;
}