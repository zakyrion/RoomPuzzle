using PlayerCamera.Configs;
using UnityEngine;

namespace PlayerCamera.Views
{
    public interface IPlayerCameraView
    {
        Transform Transform { get; }
        void Initialize(IPlayerCameraConfig cameraConfig);
        void SetPosition(Vector3 position);
    }
}
