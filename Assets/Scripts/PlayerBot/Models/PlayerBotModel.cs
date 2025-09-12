// METADATA file_path: `Assets/Scripts/PlayerBot/Models/PlayerBotModel.cs`

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RoomPuzzle.PlayerBot.Configs;
using PlayerMovement.Models;
using PlayerScore.Models;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.PlayerBot.Models
{
    [UsedImplicitly]
    internal class PlayerBotModel : IPlayerBotModel, ITickable, IDisposable
    {
        public event Action<Vector2> OnMovementInput;
        public event Action OnJumpInput;
        public event Action OnRunInput;
        
        public bool IsEnabled { get; private set; }
        public bool IsTargetSeeking { get; private set; }
        public Vector3 CurrentTarget { get; private set; }
        
        private readonly IPlayerBotConfig _config;
        private readonly IPlayerMovementModel _playerMovementModel;
        private readonly IPlayerScoreModel _playerScoreModel;
        
        private float _nextActionTime;
        private float _nextDirectionChangeTime;
        private Vector2 _currentMovementDirection;
        private BotBehaviorState _currentState;
        private Vector3 _lastPlayerPosition;
        private float _stuckCheckTime;
        private Vector3 _customTarget;
        private bool _hasCustomTarget;
        
        private enum BotBehaviorState
        {
            Idle,
            RandomMovement,
            SeekingTarget,
            Stuck
        }
        
        public PlayerBotModel(
            IPlayerBotConfig config,
            IPlayerMovementModel playerMovementModel,
            IPlayerScoreModel playerScoreModel)
        {
            _config = config;
            _playerMovementModel = playerMovementModel;
            _playerScoreModel = playerScoreModel;
            
            // Subscribe to position updates
            _playerMovementModel.OnPlayerPositionChanged += OnPlayerPositionChanged;
            
            ResetBehavior();
        }
        
        private void OnPlayerPositionChanged(Vector3 newPosition)
        {
            _lastPlayerPosition = newPosition;
        }
        
        public void SetEnabled(bool enabled)
        {
            IsEnabled = enabled;
            if (!enabled)
            {
                OnMovementInput?.Invoke(Vector2.zero);
                ResetBehavior();
            }
        }
        
        public void SetTargetSeekingMode(bool enabled)
        {
            IsTargetSeeking = enabled;
            if (enabled)
            {
                _currentState = BotBehaviorState.SeekingTarget;
            }
        }
        
        public void SetCustomTarget(Vector3 target)
        {
            _customTarget = target;
            _hasCustomTarget = true;
            CurrentTarget = target;
            _currentState = BotBehaviorState.SeekingTarget;
        }
        
        public void UpdateBehavior()
        {
            if (!IsEnabled) return;
            
            CheckIfStuck();
            UpdateBehaviorState();
        }
        
        public void ResetBehavior()
        {
            _currentState = BotBehaviorState.Idle;
            _currentMovementDirection = Vector2.zero;
            _nextActionTime = Time.time;
            _nextDirectionChangeTime = Time.time;
            _hasCustomTarget = false;
            CurrentTarget = Vector3.zero;
        }
        
        public void Tick()
        {
            UpdateBehavior();
        }
        
        public void Dispose()
        {
            _playerMovementModel.OnPlayerPositionChanged -= OnPlayerPositionChanged;
        }
        
        private void UpdateBehaviorState()
        {
            switch (_currentState)
            {
                case BotBehaviorState.Idle:
                    HandleIdleState();
                    break;
                    
                case BotBehaviorState.RandomMovement:
                    HandleRandomMovementState();
                    break;
                    
                case BotBehaviorState.SeekingTarget:
                    HandleSeekingTargetState();
                    break;
                    
                case BotBehaviorState.Stuck:
                    HandleStuckState();
                    break;
            }
        }
        
        private void HandleIdleState()
        {
            if (Time.time >= _nextActionTime)
            {
                if (IsTargetSeeking && FindNearestTarget())
                {
                    _currentState = BotBehaviorState.SeekingTarget;
                }
                else
                {
                    _currentState = BotBehaviorState.RandomMovement;
                    _nextDirectionChangeTime = Time.time + _config.DirectionChangeInterval;
                }
                
                _nextActionTime = Time.time + _config.ActionInterval;
            }
            
            OnMovementInput?.Invoke(Vector2.zero);
        }
        
        private void HandleRandomMovementState()
        {
            if (Time.time >= _nextDirectionChangeTime)
            {
                GenerateRandomMovementDirection();
                _nextDirectionChangeTime = Time.time + _config.DirectionChangeInterval;
            }
            
            if (IsTargetSeeking && FindNearestTarget())
            {
                _currentState = BotBehaviorState.SeekingTarget;
                return;
            }
            
            OnMovementInput?.Invoke(_currentMovementDirection);
            
            // Randomly trigger running
            if (UnityEngine.Random.value < _config.RunProbability * Time.deltaTime)
            {
                OnRunInput?.Invoke();
            }
        }
        
        private void HandleSeekingTargetState()
        {
            if (!_hasCustomTarget && IsTargetSeeking && !FindNearestTarget())
            {
                _currentState = BotBehaviorState.RandomMovement;
                return;
            }
            
            if (CurrentTarget == Vector3.zero)
            {
                _currentState = BotBehaviorState.Idle;
                return;
            }
            
            Vector3 playerPosition = _lastPlayerPosition; // Use cached position since Position property is not available
            Vector3 directionToTarget = (CurrentTarget - playerPosition).normalized;
            
            // Check if target is reached
            float distanceToTarget = Vector3.Distance(playerPosition, CurrentTarget);
            if (distanceToTarget < _config.TargetReachDistance)
            {
                if (_hasCustomTarget)
                {
                    _hasCustomTarget = false;
                    CurrentTarget = Vector3.zero;
                }
                _currentState = BotBehaviorState.Idle;
                OnMovementInput?.Invoke(Vector2.zero);
                return;
            }
            
            // Convert 3D direction to 2D movement input
            Vector2 movementInput = new Vector2(directionToTarget.x, directionToTarget.z);
            movementInput = Vector2.ClampMagnitude(movementInput, 1f);
            
            OnMovementInput?.Invoke(movementInput);
            
            // Trigger running when seeking targets
            if (UnityEngine.Random.value < _config.RunProbability * Time.deltaTime * 2f)
            {
                OnRunInput?.Invoke();
            }
            
            // Trigger jumping for obstacles
            if (ShouldJumpForObstacle(directionToTarget))
            {
                OnJumpInput?.Invoke();
            }
        }
        
        private void HandleStuckState()
        {
            // Try to get unstuck by jumping and moving in random direction
            OnJumpInput?.Invoke();
            GenerateRandomMovementDirection();
            OnMovementInput?.Invoke(_currentMovementDirection);
            
            _stuckCheckTime = Time.time + _config.StuckRecoveryTime;
            _currentState = BotBehaviorState.RandomMovement;
        }
        
        private bool FindNearestTarget()
        {
            if (_hasCustomTarget)
            {
                CurrentTarget = _customTarget;
                return true;
            }
            
            // This would need to be implemented based on your pickup system
            // For now, we'll use a placeholder that generates random targets
            if (UnityEngine.Random.value < 0.3f) // 30% chance to find a "target"
            {
                Vector3 playerPos = _lastPlayerPosition; // Use cached position
                Vector3 randomOffset = new Vector3(
                    UnityEngine.Random.Range(-_config.SearchRadius, _config.SearchRadius),
                    0,
                    UnityEngine.Random.Range(-_config.SearchRadius, _config.SearchRadius)
                );
                CurrentTarget = playerPos + randomOffset;
                return true;
            }
            
            return false;
        }
        
        private void GenerateRandomMovementDirection()
        {
            float angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
            _currentMovementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            _currentMovementDirection *= UnityEngine.Random.Range(0.3f, 1f); // Vary intensity
        }
        
        private bool ShouldJumpForObstacle(Vector3 direction)
        {
            // Simple obstacle detection - could be enhanced with raycasting
            return UnityEngine.Random.value < _config.JumpProbability * Time.deltaTime;
        }
        
        private void CheckIfStuck()
        {
            // Note: We need to get position updates through the OnPlayerPositionChanged event
            // For now, we'll disable stuck detection until proper position tracking is implemented
            if (Time.time >= _stuckCheckTime)
            {
                _stuckCheckTime = Time.time + _config.StuckCheckInterval;
            }
        }
    }
}