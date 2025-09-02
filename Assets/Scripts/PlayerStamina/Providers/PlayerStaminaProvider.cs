using JetBrains.Annotations;
using MainCanvas.Providers;
using PlayerStamina.Configs;
using PlayerStamina.UIs;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace PlayerStamina.Providers
{
    [UsedImplicitly]
    internal class PlayerStaminaProvider : IPlayerStaminaProvider
    {
        private const string PLAYER_STAMINA_UI_PATH = "Prefabs/UI/PlayerStaminaUI";
        private readonly IPlayerStaminaProviderConfig _config;
        private readonly IMainCanvasProvider _mainCanvasProvider;
        private GameObject _staminaUIGameObject;
        private IPlayerStaminaUI _staminaUI;

        [Inject]
        public PlayerStaminaProvider(IPlayerStaminaProviderConfig config, 
            IMainCanvasProvider mainCanvasProvider)
        {
            _config = config;
            _mainCanvasProvider = mainCanvasProvider;
        }

        public IPlayerStaminaUI GetStaminaUI()
        {
            if (_staminaUI != null) return _staminaUI;

            _staminaUIGameObject = Object.Instantiate(
                Resources.Load<GameObject>(PLAYER_STAMINA_UI_PATH), 
                _mainCanvasProvider.GetCanvas().transform);
            _staminaUI = _staminaUIGameObject.GetComponent<IPlayerStaminaUI>();
            return _staminaUI;
        }

        public void Dispose()
        {
            if (_staminaUIGameObject != null)
            {
                Object.Destroy(_staminaUIGameObject);
                _staminaUIGameObject = null;
                _staminaUI = null;
            }
        }
    }
}