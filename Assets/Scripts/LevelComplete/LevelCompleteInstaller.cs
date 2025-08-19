using Zenject;
using RoomPuzzle.LevelComplete.Presenter;
using RoomPuzzle.LevelComplete.View;
using RoomPuzzle.Core.UI;

public class LevelCompleteInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Provide a Canvas wrapper
        Container.Bind<ICanvasProvider>().To<CanvasProvider>().AsCached().NonLazy();

        // Loader -> Presenter chain
        Container.Bind<ILevelCompleteLoader>().To<LevelCompleteLoader>().AsCached().NonLazy();

        // UI gets created by loader
        Container.Bind<ILevelCompleteUI>().FromMethod(ctx =>
        {
            var loader = ctx.Container.Resolve<ILevelCompleteLoader>();
            return loader.Load();
        }).AsCached().NonLazy();

        // Presenter orchestrating UI + LevelManager
        Container.BindInterfacesAndSelfTo<LevelCompletePresenter>().AsCached().NonLazy();
    }
}