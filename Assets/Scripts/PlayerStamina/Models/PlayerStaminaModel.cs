using System;
using JetBrains.Annotations;
using PlayerStamina.Configs;
using UnityEngine;
using Zenject;

namespace PlayerStamina.Models
{
    [UsedImplicitly]
    internal class PlayerStaminaModel : IPlayerStaminaModel, IInitializable
    {
        private readonly IPlayerStaminaConfig _config;
        private float _currentStamina;
        private bool _isRegenerating;
        private float _regenerationTimer;

        public float CurrentStamina => _currentStamina;
        public float MaxStamina => _config.MaxStamina;
        public bool IsRegenerating => _isRegenerating;

        public event Action<float, float> OnStaminaChanged;
        public event Action OnStaminaDepleted;
        public event Action OnStaminaRegenerationStarted;
        public event Action OnStaminaRegenerationStopped;

        [Inject]
        public PlayerStaminaModel(IPlayerStaminaConfig config)
        {
            _config = config;
        }

        public void Initialize()
        {
            _currentStamina = _config.MaxStamina;
            _isRegenerating = false;
            _regenerationTimer = 0f;
        }

        public bool CanConsumeStamina(float amount) => _currentStamina >= amount;

        public void ConsumeStamina(float amount)
        {
            if (!CanConsumeStamina(amount)) return;
            
            _currentStamina = Mathf.Max(0, _currentStamina - amount);
            OnStaminaChanged?.Invoke(_currentStamina, _config.MaxStamina);
            
            if (_currentStamina <= 0)
                OnStaminaDepleted?.Invoke();
                
            StopRegeneration();
        }

        public void StartRegeneration()
        {
            if (_isRegenerating || _currentStamina >= _config.MaxStamina) return;
            
            _regenerationTimer = 0f;
            _isRegenerating = true;
            OnStaminaRegenerationStarted?.Invoke();
        }

        public void StopRegeneration()
        {
            if (!_isRegenerating) return;
            
            _isRegenerating = false;
            _regenerationTimer = 0f;
            OnStaminaRegenerationStopped?.Invoke();
        }

        public void UpdateRegeneration()
        {
            if (!_isRegenerating || _currentStamina >= _config.MaxStamina) return;
            
            _regenerationTimer += Time.deltaTime;
            
            if (_regenerationTimer >= _config.RegenerationDelay)
            {
                _currentStamina = Mathf.Min(_config.MaxStamina,
                    _currentStamina + _config.RegenerationRate * Time.deltaTime);
                OnStaminaChanged?.Invoke(_currentStamina, _config.MaxStamina);
            }
        }

        public IPlayerStaminaConfig GetConfig() => _config;
    }
}