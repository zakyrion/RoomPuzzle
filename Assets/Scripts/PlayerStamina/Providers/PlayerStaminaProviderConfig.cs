using UnityEngine;

namespace PlayerStamina.Providers
{
    [CreateAssetMenu(fileName = "PlayerStaminaProviderConfig", menuName = "Configs/Player/StaminaProvider")]
    public class PlayerStaminaProviderConfig : ScriptableObject, IPlayerStaminaProviderConfig
    {
        [Header("Prefab References")]
        [SerializeField] private GameObject _staminaViewPrefab;
        [SerializeField] private GameObject _staminaUIPrefab;

        public GameObject StaminaViewPrefab => _staminaViewPrefab;
        public GameObject StaminaUIPrefab => _staminaUIPrefab;
    }
}