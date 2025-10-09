using Core.Presenters;
using JetBrains.Annotations;
using PlayerCamera.Models;
using PlayerCamera.Providers;
using PlayerCamera.Views;
using Zenject;

namespace PlayerCamera.Presenters
{
    [UsedImplicitly]
    public class PlayerCameraPresenter : BasePresenter<IPlayerCameraModel, IPlayerCameraProvider, IPlayerCameraView>, IPlayerCameraPresenter, ITickable
    {
        public PlayerCameraPresenter(IPlayerCameraModel playerCameraModel, IPlayerCameraProvider playerCameraProvider)
            : base(playerCameraModel, playerCameraProvider)
        {
        }

        protected override IPlayerCameraView GetViewFromProvider()
        {
            return Provider.GetPlayerCameraView();
        }

        protected override void OnViewCreated()
        {
            View.Initialize(Model.GetConfig());
        }

        public void Tick()
        {
            if (Model.Following)
            {
                View.SetPosition(Model.CameraPosition);
            }
        }
    }
}
