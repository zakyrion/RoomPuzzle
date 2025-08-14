using System;
using UnityEngine;
using Zenject;
using RoomPuzzle.Features.Player.Model;
using RoomPuzzle.Features.Pickups.View;
namespace RoomPuzzle.Features.Player.View
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public event Action<PickupView> OnPickupCollected;
        [Header("Collision Settings")]
        [SerializeField] private LayerMask _platformLayerMask;
        [SerializeField] private string _oneWayPlatformTag = "OneWayPlatform";
        [SerializeField] private string _pickupTag = "Pickup";
        [SerializeField] private float _groundCheckDistance = 0.2f;
        private IPlayerModel _playerModel;
        private Rigidbody _rigidbody;
        private CapsuleCollider _collider;
        private Collider[] _oneWayPlatforms;
        private Vector3 _moveDirection;
        public Transform Transform => transform;
        public bool IsGrounded { get; private set; }
        [Inject]
        public void Construct(IPlayerModel playerModel)
        {
            _playerModel = playerModel;
        }
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        private void Start()
        {
            var platformObjects = GameObject.FindGameObjectsWithTag(_oneWayPlatformTag);
            _oneWayPlatforms = new Collider[platformObjects.Length];
            for (int i = 0; i < platformObjects.Length; i++)
            {
                _oneWayPlatforms[i] = platformObjects[i].GetComponent<Collider>();
            }
        }
        public void SetMoveDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }
        public void Jump(float force)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, 0);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
        private void FixedUpdate()
        {
            float targetSpeed = _moveDirection.x;
            float speedDifference = targetSpeed - _rigidbody.linearVelocity.x;
            float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _playerModel.Acceleration : _playerModel.Deceleration;
            float movementForce = speedDifference * accelerationRate;
            _rigidbody.AddForce(movementForce * Vector2.right, ForceMode.Force);
            CheckIfGrounded();
            HandleOneWayPlatforms();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_pickupTag))
            {
                var pickup = other.GetComponent<PickupView>();
                if (pickup != null)
                {
                    OnPickupCollected?.Invoke(pickup);
                }
            }
        }
        private void CheckIfGrounded()
        {
            float radius = _collider.radius * 0.9f;
            Vector3 origin = transform.position + new Vector3(0, _collider.radius, 0);
            IsGrounded = Physics.SphereCast(origin, radius, Vector3.down, out _, _groundCheckDistance, _platformLayerMask);
        }
        private void HandleOneWayPlatforms()
        {
            if (_oneWayPlatforms == null) return;
            foreach (var platformCollider in _oneWayPlatforms)
            {
                if (platformCollider == null) continue;
                platformCollider.enabled = _rigidbody.linearVelocity.y < 0.01f;
            }
        }
    }
}    