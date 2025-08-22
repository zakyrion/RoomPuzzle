using System.Collections.Generic;

namespace PlayerScore.Configs
{
    public interface IPlayerScoreConfig
    {
        string PlayerTag { get; }
        IReadOnlyList<PlayerScorePickupItemSpawnData> GetSpawnData();
    }
}
