using RoomPuzzle.Features.Camera.Presenter;
using RoomPuzzle.Features.Camera.View;
using RoomPuzzle.Features.Input.Model;
using RoomPuzzle.Features.Player.Model;
using RoomPuzzle.Features.Player.Presenter;
using RoomPuzzle.Features.Player.View;
using RoomPuzzle.Features.Score.Model;
using RoomPuzzle.Features.Score.Presenter;
using RoomPuzzle.Features.Score.View;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.Core.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private ScoreView _scoreView; // New: Drag your UI Text here

        public override void InstallBindings()
        {
            // Input
            Container.BindInterfacesAndSelfTo<InputModel>().AsSingle();
            // Score
            Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
            Container.Bind<IScoreView>().FromInstance(_scoreView).AsSingle();
            Container.BindInterfacesAndSelfTo<ScorePresenter>().AsSingle().NonLazy();
            // Player
            Container.Bind<IPlayerModel>().To<PlayerModel>().AsSingle();
            Container.Bind<IPlayerView>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle().NonLazy();
            // Camera
            Container.Bind<ICameraView>().FromInstance(_cameraView).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraPresenter>().AsSingle().NonLazy();
        }
    }
}
