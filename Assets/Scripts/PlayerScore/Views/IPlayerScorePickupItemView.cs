using System;
using PlayerScore.Configs;
using UnityEngine;

namespace PlayerScore.Views
{
    public interface IPlayerScorePickupItemView
    {
        event Action<IPlayerScorePickupItemView> OnPickup;
        GameObject GameObject { get; }
        int Score { get; }
        PlayerScorePickupItemType Type { get; }
        void Initialize(PlayerScorePickupItemSpawnData spawnData, string playerTag);
    }
}
