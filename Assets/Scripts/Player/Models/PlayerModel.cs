using System;
using JetBrains.Annotations;
using Player.Configs;
using UnityEngine;

namespace Player.Models
{
    [UsedImplicitly]
    public class PlayerModel : IPlayerModel
    {
        public event Action<Vector3> OnPlayerPositionChanged;
        public event Action OnPlayerSpawned;

        private readonly IPlayerConfig _playerConfig;
        private Vector3 _position;
        public float JumpForce => _playerConfig.JumpForce;

        public float Speed => _playerConfig.Speed;

        public PlayerModel(IPlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public IPlayerConfig GetConfig()
        {
            return _playerConfig;
        }

        public void PlayerSpawned()
        {
            OnPlayerSpawned?.Invoke();
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
            OnPlayerPositionChanged?.Invoke(position);
        }
    }
}
