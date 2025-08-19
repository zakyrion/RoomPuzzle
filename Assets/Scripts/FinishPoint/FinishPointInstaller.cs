using RoomPuzzle.FinishPoint.Model;
using RoomPuzzle.FinishPoint.Presenter;
using RoomPuzzle.FinishPoint.View;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.FinishPoint
{
    public class FinishPointInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<IFinishPointLoader>().To<FinishPointLoader>().AsSingle();
            Container.Bind<IFinishPointModel>().To<FinishPointModel>().AsSingle();
            Container.Bind<IFinishPointView>().FromMethod(ctx =>
            {
                var loader = ctx.Container.Resolve<IFinishPointLoader>();
                return loader.Load();
            }).AsSingle().NonLazy();
            Container.Bind<IFinishPointPresenter>().To<FinishPointPresenter>().AsSingle()
                .OnInstantiated<IFinishPointPresenter>((ctx, presenter) => presenter.Initialize()).NonLazy();
        }
    }
}
