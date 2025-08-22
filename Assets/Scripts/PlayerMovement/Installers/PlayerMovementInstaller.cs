using PlayerMovement.Configs;
using PlayerMovement.Models;
using PlayerMovement.Presenters;
using PlayerMovement.Providers;
using UnityEngine;
using Zenject;

namespace PlayerMovement.Installers
{
    public class PlayerMovementInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerMovementConfig _playerMovementConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerMovementConfig>().FromInstance(_playerMovementConfig).AsCached();
            Container.BindInterfacesTo<PlayerMovementProvider>().AsCached();
            Container.BindInterfacesTo<PlayerMovementModel>().AsCached();
            Container.BindInterfacesTo<PlayerMovementPresenter>().AsCached().NonLazy();
        }
    }
}
