using Core.Presenters;
using PlayerInput.Models;
using PlayerMovement.Models;
using PlayerMovement.Providers;
using PlayerMovement.Views;
using Zenject;

namespace PlayerMovement.Presenters
{
    public abstract class BasePlayerMovementPresenter : BasePresenter<IPlayerMovementModel, IPlayerMovementProvider, IPlayerMovementView>, IPlayerMovementPresenter, ITickable
    {
        protected readonly IPlayerInputModel PlayerInputModel;

        protected BasePlayerMovementPresenter(
            IPlayerMovementProvider playerMovementProvider,
            IPlayerMovementModel playerModel,
            IPlayerInputModel playerInputModel)
            : base(playerModel, playerMovementProvider)
        {
            PlayerInputModel = playerInputModel;
        }

        protected override IPlayerMovementView GetViewFromProvider()
        {
            return Provider.GetPlayerView();
        }

        protected override void OnViewCreated()
        {
            View.Initialize(Model.GetConfig());
            Model.PlayerSpawned();
            Model.SetPosition(View.Position);
        }

        protected override void OnBeforeDispose()
        {
            Provider.Dispose();
        }

        public void Tick()
        {
            HandleJump();
            HandleMovement();
            UpdatePlayerState();
        }

        protected abstract void HandleJump();

        protected virtual void HandleMovement()
        {
            if (PlayerInputModel.Move.sqrMagnitude > 0)
            {
                View.Move(PlayerInputModel.Move, Model.Speed);
            }
            else
            {
                View.Idle();
            }
        }

        protected virtual void UpdatePlayerState()
        {
            Model.SetPosition(View.Position);
            Model.SetGrounded(View.Grounded);
        }
    }
}
