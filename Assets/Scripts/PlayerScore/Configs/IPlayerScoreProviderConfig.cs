using UnityEngine;

namespace PlayerScore.Configs
{
    public interface IPlayerScoreProviderConfig
    {
        GameObject GetPrefab(PlayerScorePickupItemType type);
    }
}
