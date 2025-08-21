using JetBrains.Annotations;
using PlayerInput.Models;
using PlayerInput.Providers;
using PlayerInput.Views;
using UnityEngine;

namespace PlayerInput.Presenters
{
    [UsedImplicitly]
    public class PlayerInputPresenter : IPlayerInputPresenter
    {
        private readonly IPlayerInputProvider _playerInputProvider;
        private readonly IPlayerInputModel _playerInputModel;
        private IPlayerInputView _playerInputView;

        public PlayerInputPresenter(IPlayerInputProvider playerInputProvider, IPlayerInputModel playerInputModel)
        {
            _playerInputProvider = playerInputProvider;
            _playerInputModel = playerInputModel;
        }

        public void Dispose()
        {
            _playerInputView.OnJumpClicked -= OnJumpHandler;
            _playerInputView.OnMoveChanged -= OnMoveHandler;

            _playerInputProvider.Dispose();
        }

        public void Initialize()
        {
            _playerInputView = _playerInputProvider.GetPlayerInputView();

            _playerInputView.OnJumpClicked += OnJumpHandler;
            _playerInputView.OnMoveChanged += OnMoveHandler;
        }

        private void OnJumpHandler()
        {
            _playerInputModel.Jump();
        }

        private void OnMoveHandler(Vector2 move)
        {
            _playerInputModel.SetMove(move);
        }
    }
}
