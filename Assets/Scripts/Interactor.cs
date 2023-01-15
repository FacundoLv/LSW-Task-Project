using UnityEngine;

public class Interactor : MonoBehaviour, IInteractor
{
    private IInteractable _currentInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IInteractable interactable)) return;

        _currentInteractable?.OnUnfocused();
        _currentInteractable = interactable;
        _currentInteractable.OnFocused();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IInteractable interactable)) return;

        if (interactable != _currentInteractable) return;
        _currentInteractable.OnUnfocused();
        _currentInteractable = null;
    }

    public void Interact()
    {
        _currentInteractable?.Interact(this);
    }
}