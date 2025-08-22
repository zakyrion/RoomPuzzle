using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerScore.Configs
{
    [CreateAssetMenu(fileName = "PlayerScoreProviderConfig", menuName = "Configs/Player Score Provider")]
    public class PlayerScoreProviderConfig : ScriptableObject, IPlayerScoreProviderConfig
    {
        [SerializeField]
        private List<PlayerScorePickupItemPrefabData> _pickupItemPrefabs;

        public GameObject GetPrefab(PlayerScorePickupItemType type)
        {
            return _pickupItemPrefabs.FirstOrDefault(p => p.Type == type)?.Prefab;
        }
    }
}
