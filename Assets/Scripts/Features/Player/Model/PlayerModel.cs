// METADATA file_path: Assets/Scripts/Features/Player/Model/PlayerModel.cs
using RoomPuzzle.Features.Player.Config;
using RoomPuzzle.Features.Player.Model;
using UnityEngine;

namespace Features.Player.Model
{
    public class PlayerModel : IPlayerModel
    {
        public float Acceleration { get; set; }
        public float Deceleration { get; set; }
        public bool IsGrounded { get; set; }
        public float JumpForce { get; set; }
        public float MoveSpeed { get; set; }
        public Vector3 Position { get; set; }

        public PlayerModel(PlayerConfig config)
        {
            Acceleration = config.Acceleration;
            Deceleration = config.Deceleration;
            JumpForce = config.JumpForce;
            MoveSpeed = config.MoveSpeed;
        }
    }
}
