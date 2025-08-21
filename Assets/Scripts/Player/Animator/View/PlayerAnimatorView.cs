using UnityEngine;

public class PlayerAnimatorView : MonoBehaviour, IPlayerAnimatorView
{
    [Header("References")]
    [SerializeField] private Animator animator;

    private static readonly int MoveHash = Animator.StringToHash("Move");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    public void DisplayMove(float value)
    {
        if (animator == null) return;
        animator.SetFloat(MoveHash, value);
    }

    public void DisplayJump(float value)
    {
        if (animator == null) return;
        animator.SetFloat(JumpHash, value);
    }
}