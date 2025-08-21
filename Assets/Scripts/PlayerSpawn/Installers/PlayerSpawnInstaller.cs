using PlayerSpawn.Configs;
using PlayerSpawn.Providers;
using UnityEngine;
using Zenject;

namespace PlayerSpawn.Installers
{
    public class PlayerSpawnInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _spawnPoint;

        public override void InstallBindings()
        {
            Container.Bind<GameObject>().WithId(PlayerSpawnConstants.SPAWN_POINT_ID).FromInstance(_spawnPoint).AsCached();
            Container.BindInterfacesTo<PlayerSpawnProvider>().AsCached();
        }
    }
}
