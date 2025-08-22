using System;
using JetBrains.Annotations;
using PlayerMovement.Configs;
using UnityEngine;

namespace PlayerMovement.Models
{
    [UsedImplicitly]
    public class PlayerMovementModel : IPlayerMovementModel
    {
        public event Action<Vector3> OnPlayerPositionChanged;
        public event Action OnPlayerSpawned;

        private readonly IPlayerMovementConfig _playerMovementConfig;
        private float _jumpTimestamp;
        private Vector3 _position;
        public bool CanJump => Time.time - _jumpTimestamp > _playerMovementConfig.JumpCooldown && Grounded;
        public bool Grounded { get; private set; } = true;
        public float JumpForce => _playerMovementConfig.JumpForce;

        public float Speed => _playerMovementConfig.Speed;

        public PlayerMovementModel(IPlayerMovementConfig playerMovementConfig)
        {
            _playerMovementConfig = playerMovementConfig;
        }

        public IPlayerMovementConfig GetConfig()
        {
            return _playerMovementConfig;
        }

        public void Jump()
        {
            _jumpTimestamp = Time.time;
        }

        public void PlayerSpawned()
        {
            OnPlayerSpawned?.Invoke();
        }

        public void SetGrounded(bool grounded)
        {
            Grounded = grounded;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
            OnPlayerPositionChanged?.Invoke(position);
        }
    }
}
