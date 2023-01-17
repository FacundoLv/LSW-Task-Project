using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [Header("Equipped items")]
    [SerializeField] private ItemSlot _headSlot;
    [SerializeField] private ItemSlot _chestSlot;
    [SerializeField] private ItemSlot _legsSlot;
    [SerializeField] private ItemSlot _feetSlot;
    [SerializeField] private Transform _equipmentConainer;

    [Header("Inventory")]
    [SerializeField] private Transform _inventoryContainer;
    [SerializeField] private ItemSlot _itemSlotPrefab;
    private List<ItemSlot> _inventorySlots = new List<ItemSlot>();

    private CanvasGroup _canvasGroup;
    private PlayerController _playerController;
    private bool _isOpen;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        var equipmentSlots = _equipmentConainer.GetComponentsInChildren<ItemSlot>();
        foreach (var slot in equipmentSlots) slot.onItemClicked += OnClickedEquipment;
    }

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerController.onToggleInventory += Toggle;

        var equipped = _playerController.GetEquippedItems();
        foreach (var item in equipped) SetEquipmentSlot(item);

        var inventory = _playerController.GetInventory();
        foreach (var item in inventory)
        {
            var slot = Instantiate(_itemSlotPrefab, _inventoryContainer);
            slot.Set(item);
            slot.onItemClicked += OnClickedInventory;
            _inventorySlots.Add(slot);
        }

        equipped.onItemAdded += SetEquipmentSlot;
        equipped.onItemRemoved += RemoveEquipment;

        inventory.onItemAdded += SetInventorySlot;
        inventory.onItemRemoved += RemoveItem;
    }

    private void OnClickedEquipment(ItemSlot slot, Item item)
    {
        _playerController.HandleUnequip(item);
    }

    private void OnClickedInventory(ItemSlot slot, Item item)
    {
        if (item is not {Type: ItemType.Equipment}) return;
        _playerController.HandleEquip(item);
    }

    private void Toggle()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
            Open();
        else
            Close();
    }

    private void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void SetEquipmentSlot(Item item)
    {
        if (item == null) return;
        var equipment = Resources.Load<EquipmentSO>(item.ItemSO);
        switch (equipment.slot)
        {
            case Slot.Head:
                SetEquipmentSlot(_headSlot, item);
                break;
            case Slot.Chest:
                SetEquipmentSlot(_chestSlot, item);
                break;
            case Slot.Legs:
                SetEquipmentSlot(_legsSlot, item);
                break;
            case Slot.Feet:
                SetEquipmentSlot(_feetSlot, item);
                break;
        }
    }

    private void SetEquipmentSlot(ItemSlot slot, Item item)
    {
        if (!slot.IsEmpty) OnClickedEquipment(slot, slot.Item);
        slot.Set(item);
    }

    private void RemoveEquipment(Item item)
    {
        var equipment = Resources.Load<EquipmentSO>(item.ItemSO);
        switch (equipment.slot)
        {
            case Slot.Head:
                _headSlot.Set(null);
                break;
            case Slot.Chest:
                _chestSlot.Set(null);
                break;
            case Slot.Legs:
                _legsSlot.Set(null);
                break;
            case Slot.Feet:
                _feetSlot.Set(null);
                break;
        }
    }

    private void SetInventorySlot(Item item)
    {
        foreach (var slot in _inventorySlots)
        {
            if (!slot.IsEmpty) continue;
            slot.Set(item);
            break;
        }
    }

    private void RemoveItem(Item item)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.Item != item) continue;
            slot.Set(null);
            break;
        }
    }
}
