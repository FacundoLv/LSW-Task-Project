using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action onToggleInventory;

    private PlayerModel _model;
    private PlayerView _view;

    private void Awake()
    {
        _model = new PlayerModel();
        _view = GetComponent<PlayerView>();
    }

    private void Start()
    {
        _view.SetModel(_model);
    }

    private void Update()
    {
        HandleMoveInput();
        HandleInteractInput();
        HandleInventoryInput();
    }

    public Currency GetCurrency()
    {
        return _model.Currency;
    }

    public Inventory GetInventory()
    {
        return _model.Inventory;
    }

    public Inventory GetEquippedItems()
    {
        return _model.EquippedItems;
    }

    private void HandleMoveInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveDirection = new Vector2(horizontal, vertical);

        if (moveDirection.sqrMagnitude > 1) moveDirection = moveDirection.normalized;

        _view.MoveTo(moveDirection);
    }

    private void HandleInteractInput()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        _view.Interact();
    }

    private void HandleInventoryInput()
    {
        if (!Input.GetKeyDown(KeyCode.I)) return;

        onToggleInventory?.Invoke();
    }
    
    public void HandleEquip(Item item)
    {
        var inventory = GetInventory();
        if (!inventory.HasInInventory(item)) return;

        var equipped = GetEquippedItems();
        var equipmentSo = Resources.Load<EquipmentSO>(item.ItemSO);
        
        if (!equipped.CanAdd())
        {
            if (!TryRemoveEquippedItem(equipped, equipmentSo)) return;
        }

        inventory.Remove(item);
        equipped.Add(item);
        _view.Equip(equipmentSo);
    }

    public void HandleUnequip(Item item)
    {
        var inventory = GetInventory();
        if (!inventory.CanAdd()) return;

        GetEquippedItems().Remove(item);
        
        inventory.Add(item);
        var itemSlot = Resources.Load<EquipmentSO>(item.ItemSO).slot;
        _view.Unequip(itemSlot);
    }

    private bool TryRemoveEquippedItem(Inventory equipped, EquipmentSO equipmentSo)
    {
        foreach (var equippedItem in equipped)
        {
            var equippedSlot = Resources.Load<EquipmentSO>(equippedItem.ItemSO).slot;
            if (equippedSlot != equipmentSo.slot) continue;
            HandleUnequip(equippedItem);
            return true;
        }

        return false;
    }
}