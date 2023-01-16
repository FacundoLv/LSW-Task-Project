using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public event Action<Item> onItemClicked;
    
    [SerializeField] private Image _image;

    private Item _item;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnItemClicked);
    }

    private void OnItemClicked()
    {
        if (_item != null) onItemClicked?.Invoke(_item);
    }

    public void Init(Item item)
    {
        _item = item;

        if (item == null)
        {
            _image.gameObject.SetActive(false);
            return;
        }

        _image.gameObject.SetActive(true);
        _image.sprite = Resources.Load<Sprite>(item.Sprite);
    }
}