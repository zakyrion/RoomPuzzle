using UnityEngine;

namespace PlayerStamina.Configs
{
    [CreateAssetMenu(fileName = "PlayerStaminaConfig", menuName = "Configs/Player/Stamina")]
    public class PlayerStaminaConfig : ScriptableObject, IPlayerStaminaConfig
    {
        [Header("Stamina Settings")]
        [SerializeField] private float _maxStamina = 100f;
        [SerializeField] private float _regenerationRate = 20f;
        [SerializeField] private float _regenerationDelay = 2f;
        
        [Header("Consumption Settings")]
        [SerializeField] private float _jumpStaminaCost = 15f;
        [SerializeField] private float _runningStaminaCostPerSecond = 10f;
        
        public float MaxStamina => _maxStamina;
        public float RegenerationRate => _regenerationRate;
        public float RegenerationDelay => _regenerationDelay;
        public float JumpStaminaCost => _jumpStaminaCost;
        public float RunningStaminaCostPerSecond => _runningStaminaCostPerSecond;
    }
}