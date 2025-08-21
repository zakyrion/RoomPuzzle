using JetBrains.Annotations;
using PlayerCamera.Models;
using PlayerCamera.Providers;
using PlayerCamera.Views;
using Zenject;

namespace PlayerCamera.Presenters
{
    [UsedImplicitly]
    public class PlayerCameraPresenter : IPlayerCameraPresenter, ITickable
    {
        private readonly IPlayerCameraModel _playerCameraModel;
        private readonly IPlayerCameraProvider _playerCameraProvider;
        private IPlayerCameraView _playerCameraView;

        public PlayerCameraPresenter(IPlayerCameraModel playerCameraModel, IPlayerCameraProvider playerCameraProvider)
        {
            _playerCameraModel = playerCameraModel;
            _playerCameraProvider = playerCameraProvider;
        }

        public void Dispose()
        {

        }

        public void Initialize()
        {
            _playerCameraView = _playerCameraProvider.GetPlayerCameraView();
            _playerCameraView.Initialize(_playerCameraModel.GetConfig());
        }

        public void Tick()
        {
            if (_playerCameraModel.Following)
            {
                _playerCameraView.SetPosition(_playerCameraModel.CameraPosition);
            }
        }
    }
}
