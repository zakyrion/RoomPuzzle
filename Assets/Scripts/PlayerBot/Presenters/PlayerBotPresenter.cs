// METADATA file_path: `Assets/Scripts/PlayerBot/Presenters/PlayerBotPresenter.cs`

using System;
using JetBrains.Annotations;
using RoomPuzzle.PlayerBot.Models;
using PlayerInput.Models;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.PlayerBot.Presenters
{
    [UsedImplicitly]
    internal class PlayerBotPresenter : IPlayerBotPresenter, IInitializable, IDisposable
    {
        public bool IsBotEnabled => _playerBotModel.IsEnabled;
        public bool IsTargetSeeking => _playerBotModel.IsTargetSeeking;
        
        private readonly IPlayerBotModel _playerBotModel;
        private readonly IPlayerInputModel _playerInputModel;
        
        public PlayerBotPresenter(
            IPlayerBotModel playerBotModel,
            IPlayerInputModel playerInputModel)
        {
            _playerBotModel = playerBotModel;
            _playerInputModel = playerInputModel;
        }
        
        public void Initialize()
        {
            SubscribeToEvents();
        }
        
        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
        
        public void EnableBot(bool enabled)
        {
            _playerBotModel.SetEnabled(enabled);
            
            if (!enabled)
            {
                // Clear any pending input when disabling bot
                _playerInputModel.SetMove(Vector2.zero);
            }
        }
        
        public void SetTargetSeekingMode(bool enabled)
        {
            _playerBotModel.SetTargetSeekingMode(enabled);
        }
        
        public void SetCustomTarget(Vector3 target)
        {
            _playerBotModel.SetCustomTarget(target);
        }
        
        public void ToggleBotControl()
        {
            EnableBot(!IsBotEnabled);
        }
        
        public void ResetBotBehavior()
        {
            _playerBotModel.ResetBehavior();
        }
        
        private void SubscribeToEvents()
        {
            _playerBotModel.OnMovementInput += HandleMovementInput;
            _playerBotModel.OnJumpInput += HandleJumpInput;
            _playerBotModel.OnRunInput += HandleRunInput;
        }
        
        private void UnsubscribeFromEvents()
        {
            _playerBotModel.OnMovementInput -= HandleMovementInput;
            _playerBotModel.OnJumpInput -= HandleJumpInput;
            _playerBotModel.OnRunInput -= HandleRunInput;
        }
        
        private void HandleMovementInput(Vector2 movementInput)
        {
            if (_playerBotModel.IsEnabled)
            {
                _playerInputModel.SetMove(movementInput);
            }
        }
        
        private void HandleJumpInput()
        {
            if (_playerBotModel.IsEnabled)
            {
                _playerInputModel.Jump(true);
            }
        }
        
        private void HandleRunInput()
        {
            if (_playerBotModel.IsEnabled)
            {
                // Note: No run input method available in current PlayerInputModel
                // This would need to be implemented if running functionality is needed
            }
        }
    }
}