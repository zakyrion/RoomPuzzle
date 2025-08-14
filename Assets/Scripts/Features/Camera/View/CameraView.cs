using UnityEngine;

namespace RoomPuzzle.Features.Camera.View
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        [SerializeField] private Vector3 _offset = new(0, 2, -10);
        [SerializeField] private float _smoothSpeed = 0.125f;
        private Transform _target;

        private void LateUpdate()
        {
            if (_target == null)
                return;
            var desiredPosition = _target.position + _offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}
