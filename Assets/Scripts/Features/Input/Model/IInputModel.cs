using System;

namespace RoomPuzzle.Features.Input.Model
{
    public interface IInputModel
    {
        float Horizontal { get; }
        float Vertical { get; }
        event Action OnJumpPressed;
    }
}
