using System;
using UnityEngine;

namespace PlayerInput.Views
{
    public interface IPlayerInputView
    {
        event Action<Vector2> OnMoveChanged;
        event Action OnJumpClicked;
    }
}
