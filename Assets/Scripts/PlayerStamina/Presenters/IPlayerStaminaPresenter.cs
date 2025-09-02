using System;
using Zenject;

namespace PlayerStamina.Presenters
{
    public interface IPlayerStaminaPresenter : IInitializable, IDisposable, ITickable
    {
    }
}