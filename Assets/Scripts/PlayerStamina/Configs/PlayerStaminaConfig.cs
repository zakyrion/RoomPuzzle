using UnityEngine;

namespace PlayerStamina.Configs
{
    [CreateAssetMenu(fileName = "PlayerStaminaConfig", menuName = "Configs/PlayerStaminaConfig", order = 2)]
    public class PlayerStaminaConfig : ScriptableObject, IPlayerStaminaConfig
    {
        [Header("Stamina Settings")]
        [SerializeField] private float _maxStamina = 100f;
        [SerializeField] private float _jumpStaminaCost = 20f;
        
        [Header("Regeneration")]
        [SerializeField] private float _regenerationRate = 10f;

        public float MaxStamina => _maxStamina;
        public float JumpStaminaCost => _jumpStaminaCost;
        public float RegenerationRate => _regenerationRate;
    }
}
