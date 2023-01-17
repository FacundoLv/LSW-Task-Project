using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerModel _model;
    private Vector2 _moveDirection;
    private Interactor _interactionHandler;
    private AnimationController _animation;
    private EquipmentView[] _equipmentSlots;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _interactionHandler = GetComponent<Interactor>();
        _animation = GetComponent<AnimationController>();
        _equipmentSlots = GetComponentsInChildren<EquipmentView>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void MoveTo(Vector2 direction)
    {
        _moveDirection = direction;
        _animation.MoveDirection = direction;
    }

    public void Interact()
    {
        if (_interactionHandler != null) _interactionHandler.Interact();
    }

    public void SetModel(PlayerModel model)
    {
        _model = model;

        foreach (var item in _model.EquippedItems)
        {
            if (item is not {Type: ItemType.Equipment}) continue;
            var equipment = Resources.Load<EquipmentSO>(item.ItemSO);
            Equip(equipment);
        }
    }

    public void Equip(EquipmentSO equipment)
    {
        var index = Array.FindIndex(_equipmentSlots, n => n.Slot.Equals(equipment.slot));
        _equipmentSlots[index].sprites = equipment.sprites;
    }
    
    public void Unequip(Slot slot)
    {
        var index = Array.FindIndex(_equipmentSlots, n => n.Slot.Equals(slot));
        _equipmentSlots[index].sprites = Array.Empty<Sprite>();
    }


    private void HandleMovement()
    {
        if (_model == null) return;
        _rb.MovePosition(_rb.position + _moveDirection * (_model.Speed * Time.deltaTime));
    }
}