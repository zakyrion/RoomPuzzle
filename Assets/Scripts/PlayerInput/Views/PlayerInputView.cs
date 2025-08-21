using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInput.Views
{
    public class PlayerInputView : MonoBehaviour, IPlayerInputView
    {
        public event Action OnJumpClicked;
        public event Action<Vector2> OnMoveChanged;

        public void OnJump(InputValue context)
        {
            OnJumpClicked?.Invoke();
        }

        public void OnMove(InputValue context)
        {
            OnMoveChanged?.Invoke(context.Get<Vector2>());
        }
    }
}
