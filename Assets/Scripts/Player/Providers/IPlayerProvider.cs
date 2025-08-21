using Player.Views;

namespace Player.Providers
{
    public interface IPlayerProvider
    {
        public IPlayerView GetPlayerView();
        public void Dispose();
    }
}
