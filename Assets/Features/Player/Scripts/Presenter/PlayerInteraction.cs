using UnityEngine;
using Zenject;

namespace RoomPuzzle
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float _interactionDistance = 3f;
        [SerializeField] private LayerMask _interactableLayer;
        private Camera _mainCamera;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                TryInteract();
        }

        [Inject]
        private void Construct()
        {
            _mainCamera = Camera.main;
        }

        private void TryInteract()
        {
            var ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
            if (Physics.Raycast(ray, out var hit, _interactionDistance, _interactableLayer))
            {
                if (hit.collider.TryGetComponent(out IPickupable pickupable))
                {
                    pickupable.Pickup();
                }
                else if (hit.collider.TryGetComponent(out IOpenable openable))
                {
                    openable.TryOpen();
                }
            }
        }
    }
}
