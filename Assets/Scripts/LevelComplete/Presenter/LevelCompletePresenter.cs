using System;
using Zenject;
using RoomPuzzle.Level;
using RoomPuzzle.LevelComplete.View;

namespace RoomPuzzle.LevelComplete.Presenter
{
    public class LevelCompletePresenter : ILevelCompletePresenter
    {
        private readonly ILevelManager _levelManager;
        private readonly ILevelCompleteUI _ui;

        public LevelCompletePresenter(ILevelManager levelManager, ILevelCompleteUI ui)
        {
            _levelManager = levelManager;
            _ui = ui;
        }

        public void Initialize()
        {
            _ui.Hide(); // ensure UI starts hidden
            _levelManager.LevelFinished += OnLevelFinished;
        }

        private void OnLevelFinished()
        {
            _ui.Show();
        }

        public void Dispose()
        {
            _levelManager.LevelFinished -= OnLevelFinished;
        }
    }
}