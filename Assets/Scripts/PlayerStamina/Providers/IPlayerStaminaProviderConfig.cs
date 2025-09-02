using UnityEngine;

namespace PlayerStamina.Providers
{
    public interface IPlayerStaminaProviderConfig
    {
        GameObject StaminaViewPrefab { get; }
        GameObject StaminaUIPrefab { get; }
    }
}