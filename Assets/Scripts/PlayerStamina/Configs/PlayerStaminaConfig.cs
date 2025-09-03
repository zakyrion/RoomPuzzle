
using UnityEngine;

namespace PlayerStamina.Configs
{
    [CreateAssetMenu(fileName = "PlayerStaminaConfig", menuName = "Configs/PlayerStaminaConfig")]
    public class PlayerStaminaConfig : ScriptableObject, IPlayerStaminaConfig
    {
        [SerializeField] private float _maxStamina = 100f;
        [SerializeField] private float _staminaDrainRate = 10f;
        [SerializeField] private float _staminaRegenRate = 15f;
        [SerializeField] private float _jumpStaminaCost = 20f;

        public float MaxStamina => _maxStamina;
        public float StaminaDrainRate => _staminaDrainRate;
        public float StaminaRegenRate => _staminaRegenRate;
        public float JumpStaminaCost => _jumpStaminaCost;
    }
}