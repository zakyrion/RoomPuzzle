using System;
using PlayerStamina.UIs;
using PlayerStamina.Views;

namespace PlayerStamina.Providers
{
    public interface IPlayerStaminaProvider : IDisposable
    {
        IPlayerStaminaUI GetStaminaUI();
    }
}