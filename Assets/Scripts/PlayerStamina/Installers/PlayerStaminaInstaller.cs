using JetBrains.Annotations;
using PlayerStamina.Configs;
using PlayerStamina.Models;
using PlayerStamina.Presenters;
using PlayerStamina.Providers;
using UnityEngine;
using Zenject;

namespace PlayerStamina.Installers
{
    [UsedImplicitly]
    public class PlayerStaminaInstaller : MonoInstaller
    {
        [Header("Configs")]
        [SerializeField] private PlayerStaminaConfig _staminaConfig;[SerializeField] private PlayerStaminaProviderConfig _staminaProviderConfig;

        public override void InstallBindings()
        {
            // Bind configs
            Container.Bind<IPlayerStaminaConfig>()
                .FromInstance(_staminaConfig)
                .AsCached()
                .NonLazy();
                
            Container.Bind<IPlayerStaminaProviderConfig>()
                .FromInstance(_staminaProviderConfig)
                .AsCached()
                .NonLazy();

            // Bind model
            Container.Bind<IPlayerStaminaModel>()
                .To<PlayerStaminaModel>()
                .AsCached()
                .NonLazy();

            // Bind provider
            Container.Bind<IPlayerStaminaProvider>()
                .To<PlayerStaminaProvider>()
                .AsCached()
                .NonLazy();

            // Bind presenter
            Container.Bind<IPlayerStaminaPresenter>()
                .To<PlayerStaminaPresenter>()
                .AsCached()
                .NonLazy();
        }
    }
}