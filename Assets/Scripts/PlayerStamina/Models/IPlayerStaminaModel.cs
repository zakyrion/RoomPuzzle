using System;

namespace PlayerStamina.Models
{
    public interface IPlayerStaminaModel
    {
        float CurrentStamina { get; }
        float MaxStamina { get; }
        bool CanJump { get; }
        bool IsGrounded { get; set; }
        
        event Action<float> OnStaminaChanged;
        event Action OnStaminaDepleted;
        
        bool TryConsumeStamina(float amount);
        void RegenerateStamina(float deltaTime);
    }
}