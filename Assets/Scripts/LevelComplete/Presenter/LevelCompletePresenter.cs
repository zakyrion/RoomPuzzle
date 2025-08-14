using RoomPuzzle.LevelComplete.View;
using RoomPuzzle.Level;
using System;
namespace RoomPuzzle.LevelComplete.Presenter
{
    public class LevelCompletePresenter : ILevelCompletePresenter, IDisposable
    {
        private readonly ILevelManager _levelManager;
        private readonly ILevelCompleteView _view;
        public LevelCompletePresenter(ILevelManager levelManager, ILevelCompleteView view)
        {
            _levelManager = levelManager;
            _view = view;
            _levelManager.LevelFinished += OnLevelFinished;
        }
        private void OnLevelFinished()
        {
            _view.Show();
        }
        public void Dispose()
        {
            _levelManager.LevelFinished -= OnLevelFinished;
        }
    }
    public interface ILevelCompletePresenter { }
}