using System;
using UnityEngine;

namespace PlayerScore.Configs
{
    [Serializable]
    public class PlayerScorePickupItemPrefabData
    {
        [SerializeField]
        public GameObject Prefab;
        [SerializeField]
        public PlayerScorePickupItemType Type;
    }
}
