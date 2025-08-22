using System;
using PlayerScore.Configs;

namespace PlayerScore.Models
{
    public interface IPlayerScoreModel
    {
        event Action<int> OnScoreChanged;
        int Score { get; }

        void AddScore(int score);

        IPlayerScoreConfig GetConfig();
        void ResetScore();
    }
}
