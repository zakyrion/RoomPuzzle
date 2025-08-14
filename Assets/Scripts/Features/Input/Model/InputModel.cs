using System;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.Features.Input.Model
{
    public class InputModel : IInputModel, ITickable
    {
        public event Action OnJumpPressed;
        public float HorizontalInput { get; private set; }

        public void Tick()
        {
            HorizontalInput = UnityEngine.Input.GetAxis("Horizontal");
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                OnJumpPressed?.Invoke();
            }
        }
    }
}
