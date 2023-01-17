using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _anim;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    public Vector2 MoveDirection { get; set; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _anim.SetFloat(Horizontal, MoveDirection.x);
        _anim.SetFloat(Vertical, MoveDirection.y);
        _anim.SetBool(IsMoving, MoveDirection.sqrMagnitude > 0);
    }
}