namespace PlayerStamina.Configs
{
    public interface IPlayerStaminaConfig
    {
        float MaxStamina { get; }
        float RegenerationRate { get; }
        float RegenerationDelay { get; }
        float JumpStaminaCost { get; }
        float RunningStaminaCostPerSecond { get; }
    }
}