using Player.Configs;
using UnityEngine;

namespace Player.Views
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        private const float GRAVITY = -9.81f;
        private const string JUMP_ANIMATION_PARAM = "Jump";
        private const string MOVE_ANIMATION_PARAM = "Move";

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private CharacterController _characterController;

        private int _jumpHash;
        private int _moveHash;
        private float _yForce;

        public Vector3 Position => this ? transform.position : Vector3.zero;

        private void Awake()
        {
            _moveHash = Animator.StringToHash(MOVE_ANIMATION_PARAM);
            _jumpHash = Animator.StringToHash(JUMP_ANIMATION_PARAM);
        }

        public void Initialize(IPlayerConfig playerConfig)
        {
            if (!this)
            {
                return;
            }

            SetRotation(playerConfig.StartRotation);
            _yForce = GRAVITY;
        }

        public void SetRotation(float rotation)
        {
            if (!this)
            {
                return;
            }

            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        public void Jump(float jumpForce)
        {
            _yForce = jumpForce;
        }

        public void Move(Vector2 move, float speed)
        {
            _yForce = Mathf.Clamp(_yForce + GRAVITY * Time.deltaTime, GRAVITY, float.MaxValue);

            if (_yForce > 0)
            {
                _animator.SetFloat(_jumpHash, move.x);
            }
            else
            {
                _animator.SetFloat(_moveHash, 0);
            }

            _animator.SetFloat(_moveHash, Mathf.Abs(move.x));
            _characterController.Move(new Vector3(move.x, _yForce, move.y) * speed * Time.deltaTime);
        }
    }
}
