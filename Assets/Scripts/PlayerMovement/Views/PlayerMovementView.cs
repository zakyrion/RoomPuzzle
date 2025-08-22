using System.Collections;
using PlayerMovement.Configs;
using UnityEngine;

namespace PlayerMovement.Views
{
    public class PlayerMovementView : MonoBehaviour, IPlayerMovementView
    {
        private const float GRAVITY = -9.81f;
        private const string JUMP_ANIMATION_PARAM = "Jump";
        private const string RUN_ANIMATION_PARAM = "Run";

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private CharacterController _characterController;

        private int _jumpHash;
        private bool _jumping;
        private int _runHash;
        private bool _running;
        private float _yForce;
        public bool Grounded => this && _characterController.isGrounded;
        public Vector3 Position => this ? transform.position : Vector3.zero;

        private bool Jumping
        {
            set
            {
                if (_jumping && !value)
                {
                    _animator.SetBool(_jumpHash, false);
                }

                _jumping = value;
            }
        }

        private bool Running
        {
            set
            {
                if (_running != value)
                {
                    _animator.SetBool(_runHash, value);
                }
            }
        }

        private void Awake()
        {
            _jumpHash = Animator.StringToHash(JUMP_ANIMATION_PARAM);
            _runHash = Animator.StringToHash(RUN_ANIMATION_PARAM);
        }

        public void Idle()
        {
            JumpCalculation();

            _characterController.Move(new Vector3(0, _yForce, 0) * Time.deltaTime);
            _animator.SetBool(_runHash, false);
        }

        public void Initialize(IPlayerMovementConfig playerMovementConfig)
        {
            if (!this)
            {
                return;
            }

            SetRotation(playerMovementConfig.StartRotation);
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
            StartCoroutine(JumpDelay(jumpForce));
        }

        public void Move(Vector2 move, float speed)
        {
            JumpCalculation();

            Running = true;
            _characterController.Move(new Vector3(move.x * speed, _yForce, move.y * speed) * Time.deltaTime);

            var rotateTo = Quaternion.LookRotation(new Vector3(move.x, 0, move.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, Time.deltaTime * 5f);
        }

        private void JumpCalculation()
        {
            _yForce = Mathf.Clamp(_yForce + GRAVITY * Time.deltaTime, GRAVITY, float.MaxValue);
            Jumping = !_characterController.isGrounded;
        }

        private IEnumerator JumpDelay(float jumpForce)
        {
            _animator.SetBool(_jumpHash, true);
            yield return new WaitForSeconds(0.1f);
            _yForce = jumpForce;
        }
    }
}
