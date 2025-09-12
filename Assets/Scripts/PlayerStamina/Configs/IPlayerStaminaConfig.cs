namespace PlayerStamina.Configs
{
    public interface IPlayerStaminaConfig
    {
        float MaxStamina { get; }
        float JumpConsumption { get; }
        float RegenerationRate { get; }
    }
}