using JetBrains.Annotations;
using PlayerScore.Models;
using PlayerScore.Providers;
using PlayerScore.UIs;

namespace PlayerScore.Presenters
{
    [UsedImplicitly]
    public class PlayerScoreUIPresenter : IPlayerScoreUIPresenter
    {
        private readonly IPlayerScoreModel _model;
        private readonly IPlayerScoreProvider _playerScoreProvider;
        private IPlayerScoreUI _playerScoreUI;

        public PlayerScoreUIPresenter(IPlayerScoreModel model, IPlayerScoreProvider playerScoreProvider)
        {
            _model = model;
            _playerScoreProvider = playerScoreProvider;
        }

        public void Dispose()
        {
            _playerScoreProvider.DisposePlayerScoreUI();
        }

        public void Initialize()
        {
            _model.OnScoreChanged += OnScoreChangedHandler;

            _playerScoreUI = _playerScoreProvider.GetPlayerScoreUI();
            _playerScoreUI.SetActive(true);
            _playerScoreUI.SetScore(0);
        }

        private void OnScoreChangedHandler(int newScore)
        {
            _playerScoreUI.SetScore(newScore);
        }
    }
}
