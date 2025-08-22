using PlayerScore.Configs;
using PlayerScore.Models;
using PlayerScore.Presenters;
using PlayerScore.Providers;
using UnityEngine;
using Zenject;

namespace PlayerScore.Installers
{
    public class PlayerScoreInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerScoreConfig _playerScoreConfig;
        [SerializeField]
        private PlayerScoreProviderConfig _playerScoreProviderConfig;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerScoreProviderConfig>().FromInstance(_playerScoreProviderConfig).AsCached();
            Container.Bind<IPlayerScoreConfig>().FromInstance(_playerScoreConfig).AsCached();
            Container.BindInterfacesTo<PlayerScoreModel>().AsCached();
            Container.BindInterfacesTo<PlayerScoreProvider>().AsCached();
            Container.BindInterfacesTo<PlayerScoreUIPresenter>().AsCached().NonLazy();
            Container.BindInterfacesTo<PlayerScorePickupItemsPresenter>().AsCached().NonLazy();
        }
    }
}
