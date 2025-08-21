using System;

public interface IPlayerAnimatorModel
{
    event Action<float> OnJumpChanged;
    event Action<float> OnMoveChanged;
    float Jump { get; }
    float Move { get; }
    void SetJump(float value);
    void SetMove(float value);
}
