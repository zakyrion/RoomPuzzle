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
        [Header("Movement Config")]
        [SerializeField]
        private PlayerMovementConfig _playerMovementConfig;

        [Header("Presenter Type")]
        [SerializeField]
        private bool _useStaminaPresenter = true;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerMovementConfig>().FromInstance(_playerMovementConfig).AsCached();
            Container.BindInterfacesTo<PlayerMovementProvider>().AsCached();
            Container.BindInterfacesTo<PlayerMovementModel>().AsCached();

            // Bind the selected presenter implementation
            if (_useStaminaPresenter)
            {
                Container.BindInterfacesTo<PlayerMovementPresenterWithStamina>().AsCached().NonLazy();
            }
            else
            {
                Container.BindInterfacesTo<PlayerMovementPresenterBasic>().AsCached().NonLazy();
            }
        }
    }
}
