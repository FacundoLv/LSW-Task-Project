using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour, IInteractable
{
    public UnityEvent onFocused;
    public UnityEvent onUnfocused;

    [SerializeField] private ShopUI _ui;

    public void OnFocused()
    {
        onFocused?.Invoke();
    }

    public void OnUnfocused()
    {
        onUnfocused?.Invoke();
    }

    public void Interact<T>(T interactor) where T : MonoBehaviour, IInteractor
    {
        var player = interactor.GetComponent<PlayerController>();
        if (player == null) return;

        var playerCurrency = player.GetCurrency();
        var playerInventory = player.GetInventory();

        _ui.Setup(playerInventory, playerCurrency);
        _ui.Open();
    }
}