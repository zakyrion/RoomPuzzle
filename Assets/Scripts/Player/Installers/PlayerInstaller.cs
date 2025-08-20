using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;

    public override void InstallBindings()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsCached();
        Container.BindInterfacesAndSelfTo<PlayerProvider>().AsCached().NonLazy();
    }
}