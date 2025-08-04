using System;
using UnityEngine;

namespace RoomPuzzle
{
    public class DoorView : MonoBehaviour, IOpenable
    {
        public event Action OnTryOpen;
        [Header("Visuals")]
        [SerializeField] private Transform _doorVisual;
        [SerializeField] private Vector3 _openRotation = new(0, 90, 0);
        private Vector3 _closedRotation;

        private void Awake()
        {
            _closedRotation = _doorVisual?.localEulerAngles ?? Vector3.zero;
        }

        public void TryOpen()
        {
            OnTryOpen?.Invoke();
        }

        public void UpdateVisuals(bool isOpen)
        {
            if (_doorVisual != null)
            {
                _doorVisual.localEulerAngles = isOpen ? _openRotation : _closedRotation;
            }
        }
    }
}
