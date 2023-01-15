using UnityEngine;

public interface IInteractable
{
    void OnFocused();
    void OnUnfocused();
    void Interact<T>(T interactor) where T : MonoBehaviour, IInteractor;
}