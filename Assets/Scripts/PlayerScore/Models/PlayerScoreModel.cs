using System;
using JetBrains.Annotations;
using PlayerScore.Configs;

namespace PlayerScore.Models
{
    [UsedImplicitly]
    public class PlayerScoreModel : IPlayerScoreModel
    {
        public event Action<int> OnScoreChanged;
        private readonly IPlayerScoreConfig _config;
        public int Score { get; private set; }

        public PlayerScoreModel(IPlayerScoreConfig config)
        {
            _config = config;
        }

        public void AddScore(int score)
        {
            Score += score;
            OnScoreChanged?.Invoke(Score);
        }

        public IPlayerScoreConfig GetConfig()
        {
            return _config;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
