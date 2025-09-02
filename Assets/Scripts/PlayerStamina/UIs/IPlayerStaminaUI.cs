namespace PlayerStamina.UIs
{
    public interface IPlayerStaminaUI
    {
        void SetActive(bool active);
        void UpdateStaminaBar(float currentStamina, float maxStamina);
        void ShowDepletedEffect();
        void ShowRegenerationEffect(bool isRegenerating);
    }
}