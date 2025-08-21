using UnityEngine;

namespace PlayerCamera.Configs
{
    public interface IPlayerCameraConfig
    {
        Vector3 InitPosition { get; }
        Vector3 Offset { get; }
    }
}
