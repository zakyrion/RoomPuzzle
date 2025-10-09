using JetBrains.Annotations;
using PlayerInput.Models;
using PlayerMovement.Models;
using PlayerMovement.Providers;
using PlayerStamina.Configs;
using PlayerStamina.Models;
using UnityEngine;

namespace PlayerMovement.Presenters
{
    [UsedImplicitly]
    public class PlayerMovementPresenterWithStamina : BasePlayerMovementPresenter
    {
        private readonly IPlayerStaminaConfig _playerStaminaConfig;
        private readonly IPlayerStaminaModel _playerStaminaModel;

        public PlayerMovementPresenterWithStamina(
            IPlayerMovementProvider playerMovementProvider,
            IPlayerMovementModel playerModel,
            IPlayerInputModel playerInputModel,
            IPlayerStaminaModel playerStaminaModel,
            IPlayerStaminaConfig playerStaminaConfig)
            : base(playerMovementProvider, playerModel, playerInputModel)
        {
            _playerStaminaModel = playerStaminaModel;
            _playerStaminaConfig = playerStaminaConfig;
        }

        protected override void HandleJump()
        {
            if (PlayerInputModel.NeedToJump &&
                Model.CanJump &&
                _playerStaminaModel.CanConsumeStamina(_playerStaminaConfig.JumpStaminaCost))
            {
                Model.Jump();
                View.Jump(Model.JumpForce);
                _playerStaminaModel.ConsumeStamina(_playerStaminaConfig.JumpStaminaCost);
            }

            // Regenerate stamina when grounded
            if (View.Grounded)
            {
                _playerStaminaModel.RegenerateStamina(Time.deltaTime);
            }
        }
    }
}
