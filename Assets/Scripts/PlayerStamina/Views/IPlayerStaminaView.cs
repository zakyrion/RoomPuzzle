namespace PlayerStamina.Views
{
    public interface IPlayerStaminaView
    {
        void Initialize();
        void UpdateStaminaEffects(float staminaPercentage);
        void ShowExhaustionEffect();
    }
}