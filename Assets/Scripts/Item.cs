using System;

public class Item
{
    public string Name;
    public string Description;
    public Slot Slot;
    public int Price;
    public string Sprite;
}

[Serializable]
public enum Slot
{
    Head,
    Chest,
    Legs,
    Shoes
}