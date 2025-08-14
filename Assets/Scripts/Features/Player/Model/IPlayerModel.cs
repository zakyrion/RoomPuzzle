using UnityEngine;

namespace RoomPuzzle.Features.Player.Model
{
    public interface IPlayerModel
    {
        // New properties for inertia
        float Acceleration { get; }
        float Deceleration { get; }
        bool IsGrounded { get; set; }
        float JumpForce { get; }
        float MoveSpeed { get; }
        Vector3 Position { get; set; }
    }
}
