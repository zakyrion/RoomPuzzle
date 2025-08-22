using UnityEngine;

namespace PlayerMovement.Configs
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Configs/PlayerMovementConfig", order = 1)]
    public class PlayerMovementConfig : ScriptableObject, IPlayerMovementConfig
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpCooldown;
        [SerializeField] private float _startRotation;

        public float JumpCooldown => _jumpCooldown;
        public float JumpForce => _jumpForce;
        public float Speed => _speed;
        public float StartRotation => _startRotation;
    }
}
