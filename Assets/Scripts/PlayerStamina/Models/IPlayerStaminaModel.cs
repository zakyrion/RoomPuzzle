using System;

namespace PlayerStamina.Models
{
    public interface IPlayerStaminaModel
    {
        event Action<float> OnStaminaChanged;
        float CurrentStamina { get; }
        float MaxStamina { get; }
        bool CanConsumeStamina(float amount);
        void ConsumeStamina(float amount);
        void RegenerateStamina(float deltaTime);
    }
}
