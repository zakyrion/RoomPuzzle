using System;
using UnityEngine;

namespace PlayerScore.Configs
{
    [Serializable]
    public class PlayerScorePickupItemSpawnData
    {
        [SerializeField]
        public int Score;
        [SerializeField]
        public PlayerScorePickupItemType Type;
        [SerializeField]
        public Vector3 SpawnPosition;
    }
}
