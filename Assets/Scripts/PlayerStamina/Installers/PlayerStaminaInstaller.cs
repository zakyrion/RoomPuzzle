
using PlayerStamina.Configs;
using PlayerStamina.Models;
using PlayerStamina.Presenters;
using UnityEngine;
using Zenject;

namespace PlayerStamina.Installers
{
    public class PlayerStaminaInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStaminaConfig _config;
        
        public override void InstallBindings()
        {
            Container.Bind<IPlayerStaminaConfig>().FromInstance(_config).AsSingle();
            Container.Bind<IPlayerStaminaModel>().To<PlayerStaminaModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStaminaPresenter>().AsSingle();
        }
    }
}