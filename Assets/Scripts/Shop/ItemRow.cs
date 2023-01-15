using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRow : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;

    private Item _item;

    public void SetRow(Item item)
    {
        _item = item;
        _image.sprite = Resources.Load<Sprite>(item.Sprite);
        _name.text = item.Name;
        _price.text = item.Price.ToString();
    }
}