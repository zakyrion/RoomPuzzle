using JetBrains.Annotations;
using UnityEngine;

namespace PlayerStamina.Views
{
    [UsedImplicitly]
    public class PlayerStaminaView : MonoBehaviour, IPlayerStaminaView
    {
        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem _exhaustionEffect;
        [SerializeField] private Renderer _playerRenderer;
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _exhaustedColor = Color.red;

        public void Initialize()
        {
            if (_playerRenderer == null)
                _playerRenderer = GetComponent<Renderer>();
                
            if (_playerRenderer != null)
                _playerRenderer.material.color = _normalColor;
        }

        public void UpdateStaminaEffects(float staminaPercentage)
        {
            if (_playerRenderer == null) return;
            
            // Interpolate color based on stamina percentage
            var currentColor = Color.Lerp(_exhaustedColor, _normalColor, staminaPercentage);
            _playerRenderer.material.color = currentColor;
        }

        public void ShowExhaustionEffect()
        {
            if (_exhaustionEffect != null)
            {
                _exhaustionEffect.Play();
            }
        }
    }
}