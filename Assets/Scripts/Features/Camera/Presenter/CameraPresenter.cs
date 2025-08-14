using RoomPuzzle.Features.Camera.View;
using RoomPuzzle.Features.Player.View;
using UnityEngine;
using Zenject;

namespace RoomPuzzle.Features.Camera.Presenter
{
    public class CameraPresenter : ICameraPresenter, IInitializable
    {
        private readonly ICameraView _cameraView;
        private readonly IPlayerView _playerView;

        public CameraPresenter(ICameraView cameraView, IPlayerView playerView)
        {
            _cameraView = cameraView;
            _playerView = playerView;
        }

        public void Initialize()
        {
            var playerTransform = (_playerView as MonoBehaviour)?.transform;
            if (playerTransform != null)
            {
                (_cameraView as CameraView)?.SetTarget(playerTransform);
            }
        }
    }
}
