using System;

namespace RoomPuzzle.Features.Score.Model
{
    public class ScoreModel : IScoreModel
    {
        public event Action<int> OnScoreChanged;
        public int CurrentScore { get; private set; }

        public void AddScore(int amount)
        {
            CurrentScore += amount;
            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
}
