
using System;

namespace PlayerStamina.Models
{
    public interface IPlayerStaminaModel
    {
        event Action<float> OnStaminaChanged;
        
        float CurrentStamina { get; }
        float MaxStamina { get; }
        bool CanPerformJump();
        
        void DrainStamina(float amount);
        void RegenerateStamina(float deltaTime);
        void Initialize();
        float GetJumpStaminaCost();
    }
}