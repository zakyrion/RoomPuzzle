// METADATA file_path: `Assets/Scripts/PlayerBot/Demo/PlayerBotDemo.cs`

using RoomPuzzle.PlayerBot.Presenters;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.PlayerBot.Demo
{
    /// <summary>
    /// Demo script showing how to control the PlayerBot via code
    /// Attach this to any GameObject in the scene to test bot functionality
    /// </summary>
    public class PlayerBotDemo : MonoBehaviour
    {
        [Inject] private IPlayerBotPresenter _playerBotPresenter;
        
        [Header("Demo Controls")]
        [SerializeField] private KeyCode _toggleBotKey = KeyCode.B;
        [SerializeField] private KeyCode _toggleTargetSeekingKey = KeyCode.T;
        [SerializeField] private KeyCode _resetBehaviorKey = KeyCode.R;
        [SerializeField] private KeyCode _setRandomTargetKey = KeyCode.G;
        
        [Header("Demo Settings")]
        [SerializeField] private float _randomTargetRadius = 10f;
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            if (Input.GetKeyDown(_toggleBotKey))
            {
                _playerBotPresenter.ToggleBotControl();
                Debug.Log($"Bot {(_playerBotPresenter.IsBotEnabled ? "ENABLED" : "DISABLED")}");
            }
            
            if (Input.GetKeyDown(_toggleTargetSeekingKey))
            {
                bool newState = !_playerBotPresenter.IsTargetSeeking;
                _playerBotPresenter.SetTargetSeekingMode(newState);
                Debug.Log($"Target Seeking {(newState ? "ENABLED" : "DISABLED")}");
            }
            
            if (Input.GetKeyDown(_resetBehaviorKey))
            {
                _playerBotPresenter.ResetBotBehavior();
                Debug.Log("Bot behavior reset");
            }
            
            if (Input.GetKeyDown(_setRandomTargetKey))
            {
                Vector3 randomTarget = GenerateRandomTarget();
                _playerBotPresenter.SetCustomTarget(randomTarget);
                Debug.Log($"Random target set: {randomTarget}");
            }
        }
        
        private Vector3 GenerateRandomTarget()
        {
            Vector3 playerPosition = transform.position;
            Vector2 randomCircle = Random.insideUnitCircle * _randomTargetRadius;
            return playerPosition + new Vector3(randomCircle.x, 0, randomCircle.y);
        }
        
        private void OnGUI()
        {
            if (_playerBotPresenter == null) return;
            
            GUILayout.BeginArea(new Rect(10, 10, 300, 200));
            GUILayout.Label("=== Player Bot Demo ===");
            GUILayout.Label($"Bot Status: {(_playerBotPresenter.IsBotEnabled ? "ENABLED" : "DISABLED")}");
            GUILayout.Label($"Target Seeking: {(_playerBotPresenter.IsTargetSeeking ? "ON" : "OFF")}");
            GUILayout.Space(10);
            GUILayout.Label("Controls:");
            GUILayout.Label($"{_toggleBotKey} - Toggle Bot");
            GUILayout.Label($"{_toggleTargetSeekingKey} - Toggle Target Seeking");
            GUILayout.Label($"{_resetBehaviorKey} - Reset Behavior");
            GUILayout.Label($"{_setRandomTargetKey} - Set Random Target");
            GUILayout.EndArea();
        }
    }
}