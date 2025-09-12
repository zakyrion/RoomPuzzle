// METADATA file_path: `Assets/Scripts/PlayerBot/UI/PlayerBotUI.cs`

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoomPuzzle.PlayerBot.UI
{
    public class PlayerBotUI : MonoBehaviour, IPlayerBotUI
    {
        public event Action OnToggleBotClicked;
        public event Action OnResetBehaviorClicked;
        public event Action<bool> OnTargetSeekingToggled;
        public event Action<Vector3> OnCustomTargetSet;
        
        [Header("UI Elements")]
        [SerializeField] private Button _toggleBotButton;
        [SerializeField] private Button _resetBehaviorButton;
        [SerializeField] private Toggle _targetSeekingToggle;
        [SerializeField] private Button _setTargetButton;
        [SerializeField] private Text _botStatusText;
        [SerializeField] private Text _toggleButtonText;
        
        [Header("Target Setting")]
        [SerializeField] private InputField _targetXInput;
        [SerializeField] private InputField _targetYInput;
        [SerializeField] private InputField _targetZInput;
        
        private void Awake()
        {
            SubscribeToUIEvents();
        }
        
        private void OnDestroy()
        {
            UnsubscribeFromUIEvents();
        }
        
        public void UpdateBotStatus(bool isEnabled, bool isTargetSeeking)
        {
            if (_botStatusText != null)
            {
                _botStatusText.text = $"Bot: {(isEnabled ? "ENABLED" : "DISABLED")} | Target Seeking: {(isTargetSeeking ? "ON" : "OFF")}";
            }
            
            if (_toggleButtonText != null)
            {
                _toggleButtonText.text = isEnabled ? "Disable Bot" : "Enable Bot";
            }
            
            if (_targetSeekingToggle != null && _targetSeekingToggle.isOn != isTargetSeeking)
            {
                _targetSeekingToggle.SetIsOnWithoutNotify(isTargetSeeking);
            }
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void SubscribeToUIEvents()
        {
            if (_toggleBotButton != null)
                _toggleBotButton.onClick.AddListener(HandleToggleBotClicked);
                
            if (_resetBehaviorButton != null)
                _resetBehaviorButton.onClick.AddListener(HandleResetBehaviorClicked);
                
            if (_targetSeekingToggle != null)
                _targetSeekingToggle.onValueChanged.AddListener(HandleTargetSeekingToggled);
                
            if (_setTargetButton != null)
                _setTargetButton.onClick.AddListener(HandleSetTargetClicked);
        }
        
        private void UnsubscribeFromUIEvents()
        {
            if (_toggleBotButton != null)
                _toggleBotButton.onClick.RemoveListener(HandleToggleBotClicked);
                
            if (_resetBehaviorButton != null)
                _resetBehaviorButton.onClick.RemoveListener(HandleResetBehaviorClicked);
                
            if (_targetSeekingToggle != null)
                _targetSeekingToggle.onValueChanged.RemoveListener(HandleTargetSeekingToggled);
                
            if (_setTargetButton != null)
                _setTargetButton.onClick.RemoveListener(HandleSetTargetClicked);
        }
        
        private void HandleToggleBotClicked()
        {
            OnToggleBotClicked?.Invoke();
        }
        
        private void HandleResetBehaviorClicked()
        {
            OnResetBehaviorClicked?.Invoke();
        }
        
        private void HandleTargetSeekingToggled(bool value)
        {
            OnTargetSeekingToggled?.Invoke(value);
        }
        
        private void HandleSetTargetClicked()
        {
            if (TryParseTargetInputs(out Vector3 target))
            {
                OnCustomTargetSet?.Invoke(target);
            }
            else
            {
                Debug.LogWarning("Invalid target coordinates entered");
            }
        }
        
        private bool TryParseTargetInputs(out Vector3 target)
        {
            target = Vector3.zero;
            
            if (_targetXInput == null || _targetYInput == null || _targetZInput == null)
                return false;
                
            if (!float.TryParse(_targetXInput.text, out float x))
                return false;
                
            if (!float.TryParse(_targetYInput.text, out float y))
                return false;
                
            if (!float.TryParse(_targetZInput.text, out float z))
                return false;
                
            target = new Vector3(x, y, z);
            return true;
        }
    }
}