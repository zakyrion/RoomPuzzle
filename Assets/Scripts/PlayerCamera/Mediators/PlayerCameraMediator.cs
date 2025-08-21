using JetBrains.Annotations;
using Player.Models;
using PlayerCamera.Models;
using UnityEngine;

namespace PlayerCamera.Mediators
{
    [UsedImplicitly]
    public class PlayerCameraMediator : IPlayerCameraMediator
    {
        private readonly IPlayerModel _playerModel;
        private readonly IPlayerCameraModel _playerCameraModel;

        public PlayerCameraMediator(IPlayerCameraModel playerCameraModel, IPlayerModel playerModel)
        {
            _playerCameraModel = playerCameraModel;
            _playerModel = playerModel;

            _playerModel.OnPlayerSpawned += PlayerSpawnedHandler;
            _playerModel.OnPlayerPositionChanged += PlayerPositionChangeHandler;
        }

        private void PlayerPositionChangeHandler(Vector3 position)
        {
            _playerCameraModel.SetFollowPoint(position);
        }

        private void PlayerSpawnedHandler()
        {
            _playerCameraModel.SetFollowing(true);
        }
    }
}
