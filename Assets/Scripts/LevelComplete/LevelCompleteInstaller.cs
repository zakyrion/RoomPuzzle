using Zenject;    
using RoomPuzzle.LevelComplete.View;    
using RoomPuzzle.LevelComplete.Presenter;    
namespace RoomPuzzle.LevelComplete    
{    
    public class LevelCompleteInstaller : MonoInstaller    
    {    
        public override void InstallBindings()    
        {    
            Container.Bind<ILevelCompleteLoader>().To<LevelCompleteLoader>().AsSingle();    
            Container.Bind<ILevelCompleteView>().FromMethod(ctx =>    
            {    
                var loader = ctx.Container.Resolve<ILevelCompleteLoader>();    
                return loader.Load();    
            }).AsSingle().NonLazy();    
            Container.Bind<ILevelCompletePresenter>().To<Presenter.LevelCompletePresenter>().AsSingle();    
        }    
    }    
}