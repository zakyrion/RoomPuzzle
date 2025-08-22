using UnityEngine;

namespace PlayerInput.Models
{
    public interface IPlayerInputModel
    {
        Vector2 Move { get; }
        bool NeedToJump { get; }

        void Jump(bool jump);
        void SetMove(Vector2 move);
    }
}
