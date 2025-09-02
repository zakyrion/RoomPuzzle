using System;
using PlayerStamina.Configs;

namespace PlayerStamina.Models
{
    public interface IPlayerStaminaModel
    {
        float CurrentStamina { get; }
        float MaxStamina { get; }
        bool IsRegenerating { get; }
        
        event Action<float, float> OnStaminaChanged;
        event Action OnStaminaDepleted;
        event Action OnStaminaRegenerationStarted;
        event Action OnStaminaRegenerationStopped;
        
        bool CanConsumeStamina(float amount);
        void ConsumeStamina(float amount);
        void StartRegeneration();
        void StopRegeneration();
        void UpdateRegeneration();
        IPlayerStaminaConfig GetConfig();
    }
}