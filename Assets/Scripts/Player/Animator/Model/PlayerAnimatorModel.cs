using System;

public class PlayerAnimatorModel : IPlayerAnimatorModel
{
    public event Action<float> OnMoveChanged;
    public event Action<float> OnJumpChanged;

    private float _move;
    private float _jump;

    public float Move => _move;
    public float Jump => _jump;

    public void SetMove(float value)
    {
        var clamped = Math.Clamp(value, 0f, 1f);
        if (_move != clamped)
        {
            _move = clamped;
            OnMoveChanged?.Invoke(_move);
        }
    }

    public void SetJump(float value)
    {
        var clamped = Math.Clamp(value, 0f, 1f);
        if (_jump != clamped)
        {
            _jump = clamped;
            OnJumpChanged?.Invoke(_jump);
        }
    }
}