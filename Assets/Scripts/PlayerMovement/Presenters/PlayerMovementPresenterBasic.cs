using JetBrains.Annotations;
using PlayerInput.Models;
using PlayerMovement.Models;
using PlayerMovement.Providers;

namespace PlayerMovement.Presenters
{
    [UsedImplicitly]
    public class PlayerMovementPresenterBasic : BasePlayerMovementPresenter
    {
        public PlayerMovementPresenterBasic(
            IPlayerMovementProvider playerMovementProvider,
            IPlayerMovementModel playerModel,
            IPlayerInputModel playerInputModel)
            : base(playerMovementProvider, playerModel, playerInputModel)
        {
        }

        protected override void HandleJump()
        {
            if (PlayerInputModel.NeedToJump && Model.CanJump)
            {
                Model.Jump();
                View.Jump(Model.JumpForce);
            }
        }
    }
}
