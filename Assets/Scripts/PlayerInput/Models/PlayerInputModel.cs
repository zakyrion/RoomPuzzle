using JetBrains.Annotations;
using UnityEngine;

namespace PlayerInput.Models
{
    [UsedImplicitly]
    public class PlayerInputModel : IPlayerInputModel
    {
        public Vector2 Move { get; private set; }
        public bool NeedToJump { get; private set; }

        public void Jump(bool jump)
        {
            NeedToJump = jump;
        }

        public void SetMove(Vector2 move)
        {
            Move = move;
        }
    }
}
