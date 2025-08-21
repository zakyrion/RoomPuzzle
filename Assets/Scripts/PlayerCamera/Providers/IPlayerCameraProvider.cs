using PlayerCamera.Views;
using UnityEngine;

namespace PlayerCamera.Providers
{
    public interface IPlayerCameraProvider
    {
        Transform GetPlayerCameraTransform();
        IPlayerCameraView GetPlayerCameraView();
        Camera GetCamera();
    }
}
