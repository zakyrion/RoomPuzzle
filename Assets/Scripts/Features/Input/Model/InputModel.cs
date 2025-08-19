// METADATA file_path: Assets/Scripts/Features/Input/Model/InputModel.cs
using System;

namespace RoomPuzzle.Features.Input.Model
{
    public class InputModel : IInputModel
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public event Action OnJumpPressed;
        public void Update()
        {
            Horizontal = UnityEngine.Input.GetAxis("Horizontal");
            Vertical = UnityEngine.Input.GetAxis("Vertical");
            if (UnityEngine.Input.GetButtonDown("Jump"))
            {
                OnJumpPressed?.Invoke();
            }
        }
    }
}
