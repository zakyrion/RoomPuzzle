using PlayerCamera.Configs;
using PlayerCamera.Views;
using UnityEngine;
using Zenject;

namespace PlayerCamera.Providers
{
    public class PlayerCameraProvider : IPlayerCameraProvider
    {
        private readonly GameObject _playerCamera;
        private readonly IPlayerCameraView _playerCameraView;

        [Inject]
        public PlayerCameraProvider([Inject(Id = PlayerCameraConstants.CAMERA_ID)] GameObject playerCamera)
        {
            _playerCamera = playerCamera;
            _playerCameraView = _playerCamera.GetComponent<PlayerCameraView>();
        }

        public Camera GetCamera()
        {
            return _playerCamera.GetComponent<Camera>();
        }

        public Transform GetPlayerCameraTransform()
        {
            return _playerCameraView.Transform;
        }

        public IPlayerCameraView GetPlayerCameraView()
        {
            return _playerCameraView;
        }
    }
}
