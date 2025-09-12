using PlayerMovement.Configs;
using PlayerMovement.Models;
using PlayerMovement.Presenters;
using PlayerMovement.Providers;
using PlayerStamina.Configs;
using PlayerStamina.Models;
using UnityEngine;
using Zenject;

namespace PlayerMovement.Installers
{
    public class PlayerMovementInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerMovementConfig _playerMovementConfig;
        [SerializeField]
        private PlayerStaminaConfig _playerStaminaConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerMovementConfig>().FromInstance(_playerMovementConfig).AsCached();
            Container.BindInterfacesTo<PlayerStaminaConfig>().FromInstance(_playerStaminaConfig).AsCached();
            Container.BindInterfacesTo<PlayerMovementProvider>().AsCached();
            Container.BindInterfacesTo<PlayerStaminaModel>().AsCached().NonLazy();
            Container.BindInterfacesTo<PlayerMovementModel>().AsCached();
            Container.BindInterfacesTo<PlayerMovementPresenter>().AsCached().NonLazy();
        }
    }
}
