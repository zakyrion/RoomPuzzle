// METADATA file_path: Assets/Scripts/Features/Player/Config/PlayerConfig.cs
using UnityEngine;

namespace RoomPuzzle.Features.Player.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement Settings")]
        public float MoveSpeed = 5f;
        public float JumpForce = 3f;
        public float Acceleration = 1f;
        public float Deceleration = 1f;
    }
}
