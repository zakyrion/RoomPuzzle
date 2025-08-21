using JetBrains.Annotations;
using UnityEngine;

namespace PlayerInput.Models
{
    [UsedImplicitly]
    public class PlayerInputModel : IPlayerInputModel
    {
        private bool _needToJump;

        public Vector2 Move { get; private set; }

        public bool NeedToJump
        {
            get
            {
                if (_needToJump)
                {
                    _needToJump = false;
                    return true;
                }

                return false;
            }
        }

        public void Jump()
        {
            _needToJump = true;
        }

        public void SetMove(Vector2 move)
        {
            Move = move;
        }
    }
}
