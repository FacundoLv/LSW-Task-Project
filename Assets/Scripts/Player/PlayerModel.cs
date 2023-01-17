using System;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerModel
{
    public PlayerModel()
    {
        var jsonFile = Resources.Load<TextAsset>("playerInventory");

        var data = JsonConvert.DeserializeObject<InventoryData>(jsonFile.text);
        Inventory = new Inventory(data);

        var equipped = data != null ? data.Equipped : Array.Empty<Item>();
        EquippedItems = new Inventory(equipped, 4);

        Currency = new Currency(1000);
    }

    public float Speed { get; set; } = 2f;
    public Inventory EquippedItems { get; }
    public Inventory Inventory { get; }
    public Currency Currency { get; }
}