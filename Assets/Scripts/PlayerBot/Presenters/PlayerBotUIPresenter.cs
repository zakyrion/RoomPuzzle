// METADATA file_path: `Assets/Scripts/PlayerBot/Presenters/PlayerBotUIPresenter.cs`

using System;
using JetBrains.Annotations;
using RoomPuzzle.PlayerBot.UI;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.PlayerBot.Presenters
{
    [UsedImplicitly]
    internal class PlayerBotUIPresenter : IInitializable, IDisposable, ITickable
    {
        private readonly IPlayerBotPresenter _playerBotPresenter;
        private readonly IPlayerBotUI _playerBotUI;
        
        private float _lastUpdateTime;
        private const float UPDATE_INTERVAL = 0.1f; // Update UI 10 times per second
        
        public PlayerBotUIPresenter(
            IPlayerBotPresenter playerBotPresenter,
            IPlayerBotUI playerBotUI)
        {
            _playerBotPresenter = playerBotPresenter;
            _playerBotUI = playerBotUI;
        }
        
        public void Initialize()
        {
            SubscribeToUIEvents();
            UpdateUI();
        }
        
        public void Dispose()
        {
            UnsubscribeFromUIEvents();
        }
        
        public void Tick()
        {
            // Update UI periodically to reflect current bot status
            if (Time.time - _lastUpdateTime >= UPDATE_INTERVAL)
            {
                UpdateUI();
                _lastUpdateTime = Time.time;
            }
        }
        
        private void SubscribeToUIEvents()
        {
            _playerBotUI.OnToggleBotClicked += HandleToggleBotClicked;
            _playerBotUI.OnResetBehaviorClicked += HandleResetBehaviorClicked;
            _playerBotUI.OnTargetSeekingToggled += HandleTargetSeekingToggled;
            _playerBotUI.OnCustomTargetSet += HandleCustomTargetSet;
        }
        
        private void UnsubscribeFromUIEvents()
        {
            _playerBotUI.OnToggleBotClicked -= HandleToggleBotClicked;
            _playerBotUI.OnResetBehaviorClicked -= HandleResetBehaviorClicked;
            _playerBotUI.OnTargetSeekingToggled -= HandleTargetSeekingToggled;
            _playerBotUI.OnCustomTargetSet -= HandleCustomTargetSet;
        }
        
        private void HandleToggleBotClicked()
        {
            _playerBotPresenter.ToggleBotControl();
        }
        
        private void HandleResetBehaviorClicked()
        {
            _playerBotPresenter.ResetBotBehavior();
        }
        
        private void HandleTargetSeekingToggled(bool enabled)
        {
            _playerBotPresenter.SetTargetSeekingMode(enabled);
        }
        
        private void HandleCustomTargetSet(Vector3 target)
        {
            _playerBotPresenter.SetCustomTarget(target);
            Debug.Log($"Bot target set to: {target}");
        }
        
        private void UpdateUI()
        {
            _playerBotUI.UpdateBotStatus(
                _playerBotPresenter.IsBotEnabled,
                _playerBotPresenter.IsTargetSeeking
            );
        }
    }
}