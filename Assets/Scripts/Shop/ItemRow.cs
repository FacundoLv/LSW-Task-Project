using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRow : MonoBehaviour
{
    public event Action<Item> onItemClicked;
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;

    private Item _item;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnItemClicked);
    }

    private void OnItemClicked()
    {
        onItemClicked?.Invoke(_item);
    }

    public void Set(Item item)
    {
        _item = item;
        _image.sprite = Resources.Load<Sprite>(item.Sprite);
        _image.preserveAspect = true;
        _name.text = item.Name;
        _price.text = item.Price.ToString();
    }
}