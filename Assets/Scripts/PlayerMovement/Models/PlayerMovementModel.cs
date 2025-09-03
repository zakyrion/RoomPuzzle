using System;
using JetBrains.Annotations;
using PlayerMovement.Configs;
using PlayerStamina.Models;
using UnityEngine;
using Zenject;

namespace PlayerMovement.Models
{
    [UsedImplicitly]
    public class PlayerMovementModel : IPlayerMovementModel
    {
        public event Action<Vector3> OnPlayerPositionChanged;
        public event Action OnPlayerSpawned;

        private readonly IPlayerMovementConfig _playerMovementConfig;
        [Inject] private IPlayerStaminaModel _staminaModel;
        
        private float _jumpTimestamp;
        private Vector3 _position;
        
        public bool CanJump => Time.time - _jumpTimestamp > _playerMovementConfig.JumpCooldown && Grounded && _staminaModel.CanPerformJump();
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
            if (_staminaModel.CanPerformJump())
            {
                _jumpTimestamp = Time.time;
                _staminaModel.DrainStamina(_staminaModel.GetJumpStaminaCost());
            }
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
