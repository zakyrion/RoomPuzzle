using JetBrains.Annotations;
using Player.Models;
using Player.Providers;
using Player.Views;
using PlayerInput.Models;
using Zenject;

namespace Player.Presenters
{
    [UsedImplicitly]
    public class PlayerPresenter : IPlayerPresenter, ITickable
    {
        private readonly IPlayerInputModel _playerInputModel;
        private readonly IPlayerModel _playerModel;
        private readonly IPlayerProvider _playerProvider;
        private IPlayerView _playerView;

        [Inject]
        public PlayerPresenter(IPlayerProvider playerProvider, IPlayerModel playerModel, IPlayerInputModel playerInputModel)
        {
            _playerInputModel = playerInputModel;
            _playerProvider = playerProvider;
            _playerModel = playerModel;
        }

        public void Dispose()
        {
            _playerProvider.Dispose();
            _playerView = null;
        }

        public void Initialize()
        {
            _playerView = _playerProvider.GetPlayerView();
            _playerView.Initialize(_playerModel.GetConfig());

            _playerModel.PlayerSpawned();
            _playerModel.SetPosition(_playerView.Position);
        }

        public void Tick()
        {
            if (_playerInputModel.NeedToJump)
            {
                _playerView.Jump(_playerModel.JumpForce);
            }

            _playerView.Move(_playerInputModel.Move, _playerModel.Speed);
            _playerModel.SetPosition(_playerView.Position);
        }
    }
}
