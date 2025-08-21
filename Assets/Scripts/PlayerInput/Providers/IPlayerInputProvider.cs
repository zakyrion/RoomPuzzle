using PlayerInput.Views;

namespace PlayerInput.Providers
{
    public interface IPlayerInputProvider
    {
        IPlayerInputView GetPlayerInputView();
        void Dispose();
    }
}
