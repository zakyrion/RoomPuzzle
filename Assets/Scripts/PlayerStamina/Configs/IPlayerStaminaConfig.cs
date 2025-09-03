
namespace PlayerStamina.Configs
{
    public interface IPlayerStaminaConfig
    {
        float MaxStamina { get; }
        float StaminaDrainRate { get; }
        float StaminaRegenRate { get; }
        float JumpStaminaCost { get; }
    }
}