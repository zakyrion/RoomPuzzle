using JetBrains.Annotations;
using PlayerSpawn.Configs;
using UnityEngine;
using Zenject;

namespace PlayerSpawn.Providers
{
    [UsedImplicitly]
    public class PlayerSpawnProvider : IPlayerSpawnProvider
    {
        private readonly GameObject _playerSpawnPoint;

        [Inject]
        public PlayerSpawnProvider([Inject(Id = PlayerSpawnConstants.SPAWN_POINT_ID)] GameObject playerSpawnPoint)
        {
            _playerSpawnPoint = playerSpawnPoint;
        }

        public Transform GetPlayerSpawnPoint()
        {
            return _playerSpawnPoint.transform;
        }
    }
}
