using System;
using PlayerMovement.Configs;
using UnityEngine;

namespace PlayerMovement.Models
{
    public interface IPlayerMovementModel
    {
        event Action<Vector3> OnPlayerPositionChanged;
        event Action OnPlayerSpawned;
        bool CanJump { get; }
        bool Grounded { get; }
        float JumpForce { get; }

        float Speed { get; }

        IPlayerMovementConfig GetConfig();
        void Jump();
        void PlayerSpawned();
        void SetGrounded(bool grounded);
        void SetPosition(Vector3 position);
    }
}
