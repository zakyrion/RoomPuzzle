using Zenject;    
namespace RoomPuzzle    
{    
    public class KeyInstaller : MonoInstaller    
    {    
        public override void InstallBindings()    
        {    
            Container.BindInterfacesAndSelfTo<KeyPresenter>().AsSingle().NonLazy();    
            Container.Bind<KeyView>().FromComponentInHierarchy().AsSingle();    
        }    
    }    
}