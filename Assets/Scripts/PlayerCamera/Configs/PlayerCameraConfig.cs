using UnityEngine;

namespace PlayerCamera.Configs
{
    [CreateAssetMenu(fileName = "PlayerCameraConfig", menuName = "Configs/PlayerCameraConfig", order = 0)]
    public class PlayerCameraConfig : ScriptableObject, IPlayerCameraConfig
    {
        [SerializeField]
        private Vector3 _offset;
        [SerializeField]
        private Vector3 _initPosition;

        public Vector3 InitPosition => _initPosition;
        public Vector3 Offset => _offset;
    }
}
