using System;
using RoomPuzzle.Features.Score.Model;
using RoomPuzzle.Features.Score.View;
using Zenject;

namespace RoomPuzzle.Features.Score.Presenter
{
    public class ScorePresenter : IScorePresenter, IInitializable, IDisposable
    {
        private readonly IScoreModel _scoreModel;
        private readonly IScoreView _scoreView;

        public ScorePresenter(IScoreModel scoreModel, IScoreView scoreView)
        {
            _scoreModel = scoreModel;
            _scoreView = scoreView;
        }

        public void Dispose()
        {
            _scoreModel.OnScoreChanged -= HandleScoreChanged;
        }

        public void Initialize()
        {
            _scoreModel.OnScoreChanged += HandleScoreChanged;
            // Set initial score text
            HandleScoreChanged(_scoreModel.CurrentScore);
        }

        private void HandleScoreChanged(int newScore)
        {
            _scoreView.UpdateScoreText($"Score: {newScore}");
        }
    }
}
