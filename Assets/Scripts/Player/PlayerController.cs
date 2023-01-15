using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _model;
    private PlayerView _view;

    private void Start()
    {
        _model = new PlayerModel();

        _view = GetComponent<PlayerView>();
        _view.SetModel(_model);
    }

    private void Update()
    {
        HandleMoveInput();
    }

    private void HandleMoveInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveDirection = new Vector2(horizontal, vertical);

        if (moveDirection.sqrMagnitude > 1) moveDirection = moveDirection.normalized;

        _view.MoveTo(moveDirection);
    }
}