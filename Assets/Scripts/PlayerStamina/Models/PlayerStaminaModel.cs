using System;
using PlayerStamina.Configs;
using UnityEngine;
using Zenject;

namespace PlayerStamina.Models
{
    public class PlayerStaminaModel : IPlayerStaminaModel
    {
        public event Action<float> OnStaminaChanged;
        
        [Inject] private IPlayerStaminaConfig _config;
        
        private float _currentStamina;
        
        public float CurrentStamina => _currentStamina;
        public float MaxStamina => _config.MaxStamina;
        
        public bool CanPerformJump() => _currentStamina >= GetJumpStaminaCost();
        
        public void Initialize()
        {
            _currentStamina = _config.MaxStamina;
            OnStaminaChanged?.Invoke(_currentStamina);
        }
        
        public void DrainStamina(float amount)
        {
            _currentStamina = Mathf.Max(0f, _currentStamina - amount);
            OnStaminaChanged?.Invoke(_currentStamina);
        }
        
        public void RegenerateStamina(float deltaTime)
        {
            if (_currentStamina < _config.MaxStamina)
            {
                _currentStamina = Mathf.Min(_config.MaxStamina, _currentStamina + _config.StaminaRegenRate * deltaTime);
                OnStaminaChanged?.Invoke(_currentStamina);
            }
        }
        
        public float GetJumpStaminaCost()
        {
            return _config.JumpStaminaCost;
        }
    }
}