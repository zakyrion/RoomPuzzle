namespace PlayerStamina.Configs
{
    public interface IPlayerStaminaConfig
    {
        float MaxStamina { get; }
        float JumpStaminaCost { get; }
        float RegenerationRate { get; }
    }
}
