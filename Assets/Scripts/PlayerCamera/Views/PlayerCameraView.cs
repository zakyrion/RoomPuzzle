using PlayerCamera.Configs;
using UnityEngine;

namespace PlayerCamera.Views
{
    public class PlayerCameraView : MonoBehaviour, IPlayerCameraView
    {
        public Transform Transform => this ? transform : null;

        public void Initialize(IPlayerCameraConfig cameraConfig)
        {
            if (!this)
            {
                return;
            }

            transform.position = cameraConfig.InitPosition;
        }

        public void SetPosition(Vector3 position)
        {
            if (!this)
            {
                return;
            }

            transform.position = position;
        }
    }
}
