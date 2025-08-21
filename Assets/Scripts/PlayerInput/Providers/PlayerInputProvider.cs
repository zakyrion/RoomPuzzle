using JetBrains.Annotations;
using PlayerCamera.Providers;
using PlayerInput.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PlayerInput.Providers
{
    [UsedImplicitly]
    public class PlayerInputProvider : IPlayerInputProvider
    {
        private const string PLAYER_INPUT_PREFAB_PATH = "Prefabs/Player/PlayerInputListener";
        private readonly IPlayerCameraProvider _playerCameraProvider;
        private GameObject _playerInputInstance;
        private IPlayerInputView _playerInputView;

        public PlayerInputProvider(IPlayerCameraProvider playerCameraProvider)
        {
            _playerCameraProvider = playerCameraProvider;
        }

        public void Dispose()
        {
            if (_playerInputInstance)
            {
                Object.Destroy(_playerInputInstance);
                _playerInputView = null;
                _playerInputInstance = null;
            }
        }

        public IPlayerInputView GetPlayerInputView()
        {
            if (_playerInputView != null)
            {
                return _playerInputView;
            }

            _playerInputInstance = Object.Instantiate(Resources.Load<GameObject>(PLAYER_INPUT_PREFAB_PATH));

            var playerInput = _playerInputInstance.GetComponent<UnityEngine.InputSystem.PlayerInput>();
            playerInput.camera = _playerCameraProvider.GetCamera();

            _playerInputView = _playerInputInstance.GetComponent<IPlayerInputView>();
            return _playerInputView;
        }
    }
}
