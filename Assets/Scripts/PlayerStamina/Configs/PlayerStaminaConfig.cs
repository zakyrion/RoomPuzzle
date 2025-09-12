using UnityEngine;

namespace PlayerStamina.Configs
{
    [CreateAssetMenu(fileName = "PlayerStaminaConfig", menuName = "Configs/Player/PlayerStaminaConfig")]
    public class PlayerStaminaConfig : ScriptableObject, IPlayerStaminaConfig
    {
        [Header("Stamina Settings")]
        [SerializeField] private float _maxStamina = 200f;
        [SerializeField] private float _jumpConsumption = 20f;
        [SerializeField] private float _regenerationRate = 10f;

        public float MaxStamina => _maxStamina;
        public float JumpConsumption => _jumpConsumption;
        public float RegenerationRate => _regenerationRate;
    }
}