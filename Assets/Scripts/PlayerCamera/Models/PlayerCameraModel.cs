using JetBrains.Annotations;
using PlayerCamera.Configs;
using UnityEngine;

namespace PlayerCamera.Models
{
    [UsedImplicitly]
    public class PlayerCameraModel : IPlayerCameraModel
    {
        private readonly IPlayerCameraConfig _config;
        public bool Following { get; private set; }
        public Vector3 CameraPosition { get; private set; }

        public PlayerCameraModel(IPlayerCameraConfig config)
        {
            _config = config;
        }

        public IPlayerCameraConfig GetConfig()
        {
            return _config;
        }

        public void SetFollowing(bool following)
        {
            Following = following;
        }

        public void SetFollowPoint(Vector3 followPoint)
        {
            CameraPosition = followPoint + _config.Offset;
        }
    }
}
