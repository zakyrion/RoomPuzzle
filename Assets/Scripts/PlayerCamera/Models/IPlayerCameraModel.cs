using PlayerCamera.Configs;
using UnityEngine;

namespace PlayerCamera.Models
{
    public interface IPlayerCameraModel
    {
        bool Following { get; }
        Vector3 CameraPosition { get; }
        IPlayerCameraConfig GetConfig();

        void SetFollowPoint(Vector3 followPoint);
        void SetFollowing(bool following);
    }
}
