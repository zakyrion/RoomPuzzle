// METADATA file_path: `Assets/Scripts/PlayerStamina/Presenters/PlayerStaminaPresenter.cs`

using PlayerMovement.Models;
using PlayerStamina.Models;
using UnityEngine;
using Zenject;

namespace PlayerStamina.Presenters
{
    public class PlayerStaminaPresenter : IPlayerStaminaPresenter, ITickable
    {
        [Inject] private IPlayerStaminaModel _model;
        [Inject] private IPlayerMovementModel _movementModel;
        
        public void Initialize()
        {
            _model.Initialize();
        }
        
        public void Tick()
        {
            // Only regenerate stamina when player is grounded
            if (_movementModel.Grounded)
            {
                _model.RegenerateStamina(Time.deltaTime);
            }
        }
        
        public void Dispose()
        {
            // Cleanup if needed
        }
    }
}