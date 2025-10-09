using PlayerStamina.Configs;
using PlayerStamina.Models;
using UnityEngine;
using Zenject;

namespace PlayerStamina.Installers
{
    public class PlayerStaminaInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerStaminaConfig _playerStaminaConfig;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerStaminaConfig>().FromInstance(_playerStaminaConfig).AsCached();
            Container.BindInterfacesTo<PlayerStaminaModel>().AsSingle();
        }
    }
}
