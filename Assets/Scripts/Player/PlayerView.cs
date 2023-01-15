using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerModel _model;
    private Vector2 _moveDirection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void SetModel(PlayerModel model)
    {
        _model = model;
    }

    public void MoveTo(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void HandleMovement()
    {
        if (_model == null) return;
        _rb.MovePosition(_rb.position + _moveDirection * (_model.Speed * Time.deltaTime));
    }
}