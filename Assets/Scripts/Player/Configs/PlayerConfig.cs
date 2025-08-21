using UnityEngine;

namespace Player.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject, IPlayerConfig
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _startRotation;

        public float JumpForce => _jumpForce;
        public float Speed => _speed;
        public float StartRotation => _startRotation;
    }
}
