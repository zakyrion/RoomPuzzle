using PlayerCamera.Configs;
using PlayerCamera.Mediators;
using PlayerCamera.Models;
using PlayerCamera.Presenters;
using PlayerCamera.Providers;
using UnityEngine;
using Zenject;

namespace PlayerCamera.Installers
{
    public class PlayerCameraInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _camera;
        [SerializeField]
        private PlayerCameraConfig _playerCameraConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameObject>().WithId(PlayerCameraConstants.CAMERA_ID).FromInstance(_camera).AsCached();
            Container.BindInterfacesTo<PlayerCameraConfig>().FromInstance(_playerCameraConfig).AsCached();
            Container.BindInterfacesTo<PlayerCameraProvider>().AsCached();
            Container.BindInterfacesTo<PlayerCameraPresenter>().AsCached().NonLazy();
            Container.BindInterfacesTo<PlayerCameraModel>().AsCached();
            Container.BindInterfacesTo<PlayerCameraMediator>().AsCached().NonLazy();
        }
    }
}
