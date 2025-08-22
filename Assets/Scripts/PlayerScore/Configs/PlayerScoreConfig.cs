using System.Collections.Generic;
using UnityEngine;

namespace PlayerScore.Configs
{
    [CreateAssetMenu(fileName = "PlayerScoreConfig", menuName = "Configs/PlayerScoreConfig")]
    public class PlayerScoreConfig : ScriptableObject, IPlayerScoreConfig
    {
        [SerializeField]
        private List<PlayerScorePickupItemSpawnData> _spawnData = new();
        [SerializeField]
        private string _playerTag = "Player";

        public string PlayerTag => _playerTag;

        public IReadOnlyList<PlayerScorePickupItemSpawnData> GetSpawnData()
        {
            return _spawnData;
        }
    }
}
