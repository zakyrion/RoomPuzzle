using PlayerMovement.Views;

namespace PlayerMovement.Providers
{
    public interface IPlayerMovementProvider
    {
        public void Dispose();
        public IPlayerMovementView GetPlayerView();
    }
}
