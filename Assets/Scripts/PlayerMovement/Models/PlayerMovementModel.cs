using System;
using JetBrains.Annotations;
using PlayerMovement.Configs;
using PlayerStamina.Models;
using UnityEngine;

namespace PlayerMovement.Models
{
    [UsedImplicitly]
    public class PlayerMovementModel : IPlayerMovementModel
    {
        public event Action<Vector3> OnPlayerPositionChanged;
        public event Action OnPlayerSpawned;

        private readonly IPlayerMovementConfig _playerMovementConfig;
        private readonly IPlayerStaminaModel _staminaModel;
        private float _jumpTimestamp;
        private Vector3 _position;
        public bool CanJump => Time.time - _jumpTimestamp > _playerMovementConfig.JumpCooldown && Grounded && _staminaModel.CanJump;
        public bool Grounded { get; private set; } = true;
        public float JumpForce => _playerMovementConfig.JumpForce;

        public float Speed => _playerMovementConfig.Speed;

        public PlayerMovementModel(IPlayerMovementConfig playerMovementConfig, IPlayerStaminaModel staminaModel)
        {
            _playerMovementConfig = playerMovementConfig;
            _staminaModel = staminaModel;
        }

        public IPlayerMovementConfig GetConfig()
        {
            return _playerMovementConfig;
        }

        public void Jump()
        {
            _jumpTimestamp = Time.time;
        }
        
        public bool TryJump()
        {
            if (_staminaModel.TryConsumeStamina(20f)) // Consume 20 stamina as specified
            {
                _jumpTimestamp = Time.time;
                return true;
            }
            return false;
        }

        public void PlayerSpawned()
        {
            OnPlayerSpawned?.Invoke();
        }

        public void SetGrounded(bool grounded)
        {
            Grounded = grounded;
            _staminaModel.IsGrounded = grounded;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
            OnPlayerPositionChanged?.Invoke(position);
        }
    }
}
