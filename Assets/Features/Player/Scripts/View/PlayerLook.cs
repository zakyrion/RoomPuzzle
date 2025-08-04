using UnityEngine;

namespace RoomPuzzle
{
    /// <summary>
    ///     VIEW: Handles camera and player body rotation based on mouse input.
    /// </summary>
    public class PlayerLook : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _mouseSensitivity = 100f;
        [Header("References")]
        [Tooltip("The transform of the player's main body, used for horizontal rotation.")]
        [SerializeField] private Transform _playerBody;
        [Tooltip("The transform of the camera, used for vertical rotation.")]
        [SerializeField] private Transform _cameraTransform;
        private float _xRotation;

        private void Start()
        {
            // Lock and hide the cursor for a better first-person experience
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (_playerBody == null || _cameraTransform == null)
            {
                Debug.LogWarning("Player Body or Camera Transform is not assigned in PlayerLook.", this);
                return;
            }
            // Get mouse input
            var mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
            // Calculate vertical rotation for the camera and clamp it
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            // Apply rotations
            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
