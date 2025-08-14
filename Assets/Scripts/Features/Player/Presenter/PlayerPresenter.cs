using System;
using RoomPuzzle.Features.Input.Model;
using RoomPuzzle.Features.Pickups.View;
using RoomPuzzle.Features.Player.Model;
using RoomPuzzle.Features.Player.View;
using RoomPuzzle.Features.Score.Model;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.Features.Player.Presenter
{
    public class PlayerPresenter : IPlayerPresenter, IInitializable, ITickable, IDisposable
    {
        private readonly IInputModel _inputModel;
        private readonly IPlayerModel _playerModel;
        private readonly IPlayerView _playerView;
        private readonly IScoreModel _scoreModel; // New dependency

        public PlayerPresenter(IInputModel inputModel, IPlayerModel playerModel, IPlayerView playerView, IScoreModel scoreModel)
        {
            _inputModel = inputModel;
            _playerModel = playerModel;
            _playerView = playerView;
            _scoreModel = scoreModel; // New dependency
        }

        public void Dispose()
        {
            _inputModel.OnJumpPressed -= HandleJump;
            _playerView.OnPickupCollected -= HandlePickupCollected;
        }

        public void Initialize()
        {
            _inputModel.OnJumpPressed += HandleJump;
            _playerView.OnPickupCollected += HandlePickupCollected;
        }

        public void Tick()
        {
            var moveDirection = new Vector3(_inputModel.HorizontalInput * _playerModel.MoveSpeed, 0, 0);
            _playerView.SetMoveDirection(moveDirection);
            var view = _playerView as PlayerView;
            if (view != null)
            {
                _playerModel.Position = view.Transform.position;
                _playerModel.IsGrounded = view.IsGrounded;
            }
        }

        private void HandleJump()
        {
            if (_playerModel.IsGrounded)
            {
                _playerView.Jump(_playerModel.JumpForce);
            }
        }

        private void HandlePickupCollected(PickupView pickup)
        {
            _scoreModel.AddScore(pickup.ScoreValue);
            pickup.Collect();
        }
    }
}
