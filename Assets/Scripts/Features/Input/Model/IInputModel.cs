using System;    
namespace RoomPuzzle.Features.Input.Model    
{    
    public interface IInputModel    
    {    
        float HorizontalInput { get; }    
        event Action OnJumpPressed;    
    }    
}