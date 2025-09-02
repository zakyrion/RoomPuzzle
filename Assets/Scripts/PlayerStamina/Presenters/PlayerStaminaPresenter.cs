using JetBrains.Annotations;
using PlayerStamina.Models;
using PlayerStamina.Providers;
using PlayerStamina.UIs;
using Zenject;

namespace PlayerStamina.Presenters
{
    [UsedImplicitly]
    internal class PlayerStaminaPresenter : IPlayerStaminaPresenter
    {
        private readonly IPlayerStaminaModel _staminaModel;
        private readonly IPlayerStaminaProvider _staminaProvider;
        private IPlayerStaminaUI _staminaUI;

        [Inject]
        public PlayerStaminaPresenter(
            IPlayerStaminaModel staminaModel,
            IPlayerStaminaProvider staminaProvider)
        {
            _staminaModel = staminaModel;
            _staminaProvider = staminaProvider;
        }

        public void Dispose()
        {
            // Unsubscribe from events
            if (_staminaModel != null)
            {
                _staminaModel.OnStaminaChanged -= OnStaminaChangedHandler;
                _staminaModel.OnStaminaDepleted -= OnStaminaDepletedHandler;
                _staminaModel.OnStaminaRegenerationStarted -= OnRegenerationStartedHandler;
                _staminaModel.OnStaminaRegenerationStopped -= OnRegenerationStoppedHandler;
            }

            _staminaProvider?.Dispose();
            _staminaUI = null;
        }

        public void Initialize()
        {
            _staminaUI = _staminaProvider.GetStaminaUI();
            _staminaModel.OnStaminaChanged += OnStaminaChangedHandler;
            _staminaModel.OnStaminaDepleted += OnStaminaDepletedHandler;
            _staminaModel.OnStaminaRegenerationStarted += OnRegenerationStartedHandler;
            _staminaModel.OnStaminaRegenerationStopped += OnRegenerationStoppedHandler;

            _staminaUI.SetActive(true);
            _staminaUI.UpdateStaminaBar(_staminaModel.CurrentStamina, _staminaModel.MaxStamina);
        }

        public void Tick()
        {
            _staminaModel.UpdateRegeneration();
        }

        private void OnRegenerationStartedHandler()
        {
            _staminaUI.ShowRegenerationEffect(true);
        }

        private void OnRegenerationStoppedHandler()
        {
            _staminaUI.ShowRegenerationEffect(false);
        }

        private void OnStaminaChangedHandler(float currentStamina, float maxStamina)
        {
            _staminaUI.UpdateStaminaBar(currentStamina, maxStamina);
        }

        private void OnStaminaDepletedHandler()
        {
            _staminaUI.ShowDepletedEffect();
        }
    }
}
