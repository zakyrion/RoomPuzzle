using JetBrains.Annotations;
using Player.Views;
using PlayerSpawn.Providers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Player.Providers
{
    [UsedImplicitly]
    public class PlayerProvider : IPlayerProvider
    {
        private const string PLAYER_PREFAB_PATH = "Prefabs/Player/MainMale_Character";
        private readonly IPlayerSpawnProvider _playerSpawnProvider;
        private GameObject _playerInstance;
        private IPlayerView _playerView;

        public PlayerProvider(IPlayerSpawnProvider playerSpawnProvider)
        {
            _playerSpawnProvider = playerSpawnProvider;
        }

        public void Dispose()
        {
            if (_playerInstance)
            {
                Object.Destroy(_playerInstance);
                _playerView = null;
                _playerInstance = null;
            }
        }

        public IPlayerView GetPlayerView()
        {
            if (_playerView != null)
            {
                return _playerView;
            }

            var playerSpawnPoint = _playerSpawnProvider.GetPlayerSpawnPoint();
            _playerInstance = Object.Instantiate(Resources.Load<GameObject>(PLAYER_PREFAB_PATH));
            _playerInstance.transform.position = playerSpawnPoint.position;
            _playerView = _playerInstance.GetComponent<IPlayerView>();

            return _playerView;
        }
    }
}
