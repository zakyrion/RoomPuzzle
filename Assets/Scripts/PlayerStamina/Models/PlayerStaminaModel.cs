using System;
using JetBrains.Annotations;
using PlayerStamina.Configs;
using UnityEngine;
using Zenject;

namespace PlayerStamina.Models
{
    [UsedImplicitly]
    internal class PlayerStaminaModel : IPlayerStaminaModel, ITickable
    {
        private readonly IPlayerStaminaConfig _config;
        private float _currentStamina;
        private bool _isGrounded;
        
        public float CurrentStamina => _currentStamina;
        public float MaxStamina => _config.MaxStamina;
        public bool CanJump => _currentStamina >= _config.JumpConsumption;
        
        public bool IsGrounded 
        { 
            get => _isGrounded;
            set => _isGrounded = value;
        }
        
        public event Action<float> OnStaminaChanged;
        public event Action OnStaminaDepleted;
        
        [Inject]
        public PlayerStaminaModel(IPlayerStaminaConfig config)
        {
            _config = config;
            _currentStamina = _config.MaxStamina;
        }
        
        public bool TryConsumeStamina(float amount)
        {
            if (_currentStamina < amount)
            {
                return false;
            }
            
            _currentStamina = Mathf.Max(0, _currentStamina - amount);
            OnStaminaChanged?.Invoke(_currentStamina);
            
            if (_currentStamina <= 0)
            {
                OnStaminaDepleted?.Invoke();
            }
            
            return true;
        }
        
        public void RegenerateStamina(float deltaTime)
        {
            if (!_isGrounded || _currentStamina >= _config.MaxStamina)
            {
                return;
            }
            
            var previousStamina = _currentStamina;
            _currentStamina = Mathf.Min(_config.MaxStamina, _currentStamina + _config.RegenerationRate * deltaTime);
            
            if (!Mathf.Approximately(previousStamina, _currentStamina))
            {
                OnStaminaChanged?.Invoke(_currentStamina);
            }
        }
        
        public void Tick()
        {
            RegenerateStamina(Time.deltaTime);
        }
    }
}