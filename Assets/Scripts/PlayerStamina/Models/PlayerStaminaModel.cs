using System;
using JetBrains.Annotations;
using PlayerStamina.Configs;
using UnityEngine;

namespace PlayerStamina.Models
{
    [UsedImplicitly]
    public class PlayerStaminaModel : IPlayerStaminaModel
    {
        public event Action<float> OnStaminaChanged;
        
        private readonly IPlayerStaminaConfig _config;
        private float _currentStamina;

        public float CurrentStamina => _currentStamina;
        public float MaxStamina => _config.MaxStamina;

        public PlayerStaminaModel(IPlayerStaminaConfig config)
        {
            _config = config;
            _currentStamina = _config.MaxStamina;
        }

        public bool CanConsumeStamina(float amount)
        {
            return _currentStamina >= amount;
        }

        public void ConsumeStamina(float amount)
        {
            _currentStamina = Mathf.Max(0, _currentStamina - amount);
            OnStaminaChanged?.Invoke(_currentStamina);
        }

        public void RegenerateStamina(float deltaTime)
        {
            if (_currentStamina < _config.MaxStamina)
            {
                _currentStamina = Mathf.Min(_config.MaxStamina, _currentStamina + _config.RegenerationRate * deltaTime);
                OnStaminaChanged?.Invoke(_currentStamina);
            }
        }
    }
}
