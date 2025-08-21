using Player.Configs;
using Player.Models;
using Player.Presenters;
using Player.Providers;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] PlayerConfig _playerConfig;

    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PlayerConfig>().FromInstance(_playerConfig).AsCached();
        Container.BindInterfacesTo<PlayerProvider>().AsCached();
        Container.BindInterfacesTo<PlayerModel>().AsCached();
        Container.BindInterfacesTo<PlayerPresenter>().AsCached().NonLazy();
    }
}