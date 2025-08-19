using UnityEngine;

namespace RoomPuzzle.Features.Camera.View
{
    public interface ICameraView
    {
        void Follow(Vector3 position);
    }
}
