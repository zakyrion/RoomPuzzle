using RoomPuzzle.Features.Camera.View;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraView : MonoBehaviour, ICameraView
{
    [SerializeField] private Vector3 offset = new(0, 5, -8);
    [SerializeField] private float smoothSpeed = 5f;

    public void Follow(Vector3 targetPosition)
    {
        var desiredPos = targetPosition + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
}
