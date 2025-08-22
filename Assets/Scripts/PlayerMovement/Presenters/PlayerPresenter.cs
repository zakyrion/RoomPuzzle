using JetBrains.Annotations;
using PlayerInput.Models;
using PlayerMovement.Models;
using PlayerMovement.Providers;
using PlayerMovement.Views;
using Zenject;

namespace PlayerMovement.Presenters
{
    [UsedImplicitly]
    public class PlayerMovementPresenter : IPlayerMovementPresenter, ITickable
    {
        private readonly IPlayerInputModel _playerInputModel;
        private readonly IPlayerMovementModel _playerModel;
        private readonly IPlayerMovementProvider _playerMovementProvider;
        private IPlayerMovementView _playerView;

        [Inject]
        public PlayerMovementPresenter(IPlayerMovementProvider playerMovementProvider, IPlayerMovementModel playerModel, IPlayerInputModel playerInputModel)
        {
            _playerInputModel = playerInputModel;
            _playerMovementProvider = playerMovementProvider;
            _playerModel = playerModel;
        }

        public void Dispose()
        {
            _playerMovementProvider.Dispose();
            _playerView = null;
        }

        public void Initialize()
        {
            _playerView = _playerMovementProvider.GetPlayerView();
            _playerView.Initialize(_playerModel.GetConfig());

            _playerModel.PlayerSpawned();
            _playerModel.SetPosition(_playerView.Position);
        }

        public void Tick()
        {
            if (_playerInputModel.NeedToJump && _playerModel.CanJump)
            {
                _playerModel.Jump();
                _playerView.Jump(_playerModel.JumpForce);
            }

            if (_playerInputModel.Move.sqrMagnitude > 0)
            {
                _playerView.Move(_playerInputModel.Move, _playerModel.Speed);
            }
            else
            {
                _playerView.Idle();
            }

            _playerModel.SetPosition(_playerView.Position);
            _playerModel.SetGrounded(_playerView.Grounded);
        }
    }
}
