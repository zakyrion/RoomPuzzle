// METADATA file_path: `Assets/Scripts/PlayerBot/Configs/PlayerBotConfig.cs`

using UnityEngine;

namespace RoomPuzzle.PlayerBot.Configs
{
    [CreateAssetMenu(fileName = "PlayerBotConfig", menuName = "RoomPuzzle/PlayerBot/Config")]
    public class PlayerBotConfig : ScriptableObject, IPlayerBotConfig
    {
        [Header("Behavior Timing")]
        [SerializeField] private float _actionInterval = 1f;
        [SerializeField] private float _directionChangeInterval = 3f;
        
        [Header("Action Probabilities")]
        [SerializeField, Range(0f, 1f)] private float _runProbability = 0.3f;
        [SerializeField, Range(0f, 1f)] private float _jumpProbability = 0.1f;
        
        [Header("Target Seeking")]
        [SerializeField] private float _searchRadius = 10f;
        [SerializeField] private float _targetReachDistance = 1.5f;
        
        [Header("Stuck Detection")]
        [SerializeField] private float _stuckThreshold = 0.5f;
        [SerializeField] private float _stuckCheckInterval = 2f;
        [SerializeField] private float _stuckRecoveryTime = 1f;
        
        [Header("Behavior Modes")]
        [SerializeField] private bool _enableTargetSeeking = true;
        [SerializeField] private bool _enableRandomMovement = true;
        
        public float ActionInterval => _actionInterval;
        public float DirectionChangeInterval => _directionChangeInterval;
        public float RunProbability => _runProbability;
        public float JumpProbability => _jumpProbability;
        public float SearchRadius => _searchRadius;
        public float TargetReachDistance => _targetReachDistance;
        public float StuckThreshold => _stuckThreshold;
        public float StuckCheckInterval => _stuckCheckInterval;
        public float StuckRecoveryTime => _stuckRecoveryTime;
        public bool EnableTargetSeeking => _enableTargetSeeking;
        public bool EnableRandomMovement => _enableRandomMovement;
    }
}