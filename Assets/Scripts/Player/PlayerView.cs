using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerModel _model;
    private Vector2 _moveDirection;
    private Interactor _interactionHandler;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _interactionHandler = GetComponent<Interactor>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void MoveTo(Vector2 direction)
    {
        _moveDirection = direction;
    }

    public void Interact()
    {
        if (_interactionHandler != null) _interactionHandler.Interact();
    }

    public void SetModel(PlayerModel model)
    {
        _model = model;
    }

    private void HandleMovement()
    {
        if (_model == null) return;
        _rb.MovePosition(_rb.position + _moveDirection * (_model.Speed * Time.deltaTime));
    }
}