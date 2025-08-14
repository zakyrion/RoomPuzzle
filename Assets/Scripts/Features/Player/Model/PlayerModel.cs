using UnityEngine;

namespace RoomPuzzle.Features.Player.Model
{
    public class PlayerModel : IPlayerModel
    {
        // Higher acceleration feels more responsive.
        public float Acceleration { get; } = 80f;
        // Higher deceleration makes stopping quicker.
        public float Deceleration { get; } = 100f;
        public bool IsGrounded { get; set; }
        public float JumpForce { get; } = 15f;
        public float MoveSpeed { get; } = 7f;
        public Vector3 Position { get; set; }
    }
}
