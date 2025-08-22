using System;
using PlayerScore.Configs;
using UnityEngine;

namespace PlayerScore.Views
{
    public class PlayerScorePickupItemView : MonoBehaviour, IPlayerScorePickupItemView
    {
        public event Action<IPlayerScorePickupItemView> OnPickup;

        private string _playerTag;

        public GameObject GameObject => this ? gameObject : null;
        public int Score { get; private set; }
        public PlayerScorePickupItemType Type { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (!this || !other.CompareTag(_playerTag))
            {
                return;
            }

            OnPickup?.Invoke(this);
        }

        public void Initialize(PlayerScorePickupItemSpawnData spawnData, string playerTag)
        {
            if (!this)
            {
                return;
            }

            _playerTag = playerTag;
            Score = spawnData.Score;
            Type = spawnData.Type;
            transform.position = spawnData.SpawnPosition;
        }
    }
}
