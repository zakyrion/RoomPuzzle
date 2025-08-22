using PlayerScore.Configs;
using PlayerScore.UIs;
using PlayerScore.Views;

namespace PlayerScore.Providers
{
    public interface IPlayerScoreProvider
    {
        void DisposePickupItem(IPlayerScorePickupItemView view);
        void DisposePlayerScoreUI();
        IPlayerScorePickupItemView GetPickupItem(PlayerScorePickupItemType type);
        IPlayerScoreUI GetPlayerScoreUI();
    }
}
