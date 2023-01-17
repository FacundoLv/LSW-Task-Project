using System;

public class Item
{
    public string Name;
    public string Description;
    public int Price;
    public ItemType Type;
    public string ItemSO;
    public string Sprite;
}

[Serializable]
public enum ItemType
{
    Equipment,
    Consumable
}

[Serializable]
public enum Slot
{
    Head,
    Chest,
    Legs,
    Feet
}