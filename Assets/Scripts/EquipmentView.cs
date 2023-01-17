using UnityEngine;

public class EquipmentView : MonoBehaviour
{
    public Slot Slot => _slot;
    [SerializeField] private Slot _slot;

    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer parentSpriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        HandleSprite();
    }

    private void HandleSprite()
    {
        var parentSprite = parentSpriteRenderer.sprite.name;
        var parts = parentSprite.Split('_');
        if (int.TryParse(parts[^1], out var number) && number < sprites.Length)
        {
            spriteRenderer.sprite = sprites[number];
            return;
        }

        spriteRenderer.sprite = null;
    }
}