using Core.Presenters;
using JetBrains.Annotations;
using PlayerScore.Models;
using PlayerScore.Providers;
using PlayerScore.UIs;

namespace PlayerScore.Presenters
{
    [UsedImplicitly]
    public class PlayerScoreUIPresenter : BasePresenter<IPlayerScoreModel, IPlayerScoreProvider, IPlayerScoreUI>, IPlayerScoreUIPresenter
    {
        public PlayerScoreUIPresenter(IPlayerScoreModel model, IPlayerScoreProvider playerScoreProvider)
            : base(model, playerScoreProvider)
        {
        }

        protected override IPlayerScoreUI GetViewFromProvider()
        {
            return Provider.GetPlayerScoreUI();
        }

        protected override void OnViewCreated()
        {
            Model.OnScoreChanged += OnScoreChangedHandler;
            View.SetActive(true);
            View.SetScore(0);
        }

        protected override void OnBeforeDispose()
        {
            Model.OnScoreChanged -= OnScoreChangedHandler;
            Provider.DisposePlayerScoreUI();
        }

        private void OnScoreChangedHandler(int newScore)
        {
            View.SetScore(newScore);
        }
    }
}
