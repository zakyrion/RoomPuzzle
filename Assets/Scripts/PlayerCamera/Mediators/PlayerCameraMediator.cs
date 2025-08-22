using JetBrains.Annotations;
using PlayerCamera.Models;
using PlayerMovement.Models;
using UnityEngine;

namespace PlayerCamera.Mediators
{
    [UsedImplicitly]
    public class PlayerCameraMediator : IPlayerCameraMediator
    {
        private readonly IPlayerCameraModel _playerCameraModel;
        private readonly IPlayerMovementModel _playerModel;

        public PlayerCameraMediator(IPlayerCameraModel playerCameraModel, IPlayerMovementModel playerModel)
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
