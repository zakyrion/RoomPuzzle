using System;

namespace RoomPuzzle.Features.Score.Model
{
    public interface IScoreModel
    {
        event Action<int> OnScoreChanged;
        int CurrentScore { get; }
        void AddScore(int amount);
    }
}
