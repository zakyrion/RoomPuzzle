using PlayerInput.Models;
using PlayerInput.Presenters;
using PlayerInput.Providers;
using Zenject;

namespace PlayerInput.Installers
{
    public class PlayerInputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerInputModel>().AsCached();
            Container.BindInterfacesTo<PlayerInputProvider>().AsCached();
            Container.BindInterfacesTo<PlayerInputPresenter>().AsCached().NonLazy();
        }
    }
}
